namespace DigiFrame {

    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class Item : UIBehaviour, IDynamicScrollViewItem 
    {

        private readonly Color[] colors = new Color[] {
		    Color.cyan,
		    Color.green,
	    };

        public Image icon;

        public Button destinationBtn;

        private bool isDone = false;



        void LoadDestinationScene()
        {
            SceneManager.LoadScene("DestinationVideo");
        }
        public IEnumerator LoadPlaces(int index)
        { //Request the API

            ParseJob job = new ParseJob();
            job.InData = Resources.Load<TextAsset>("destination").text;
            job.Start();

            yield return StartCoroutine(job.WaitFor());

            IDictionary response = (IDictionary)((IDictionary)job.OutData);
            IList results = (IList)response["results"];
            IDictionary destination = ((IDictionary)results[index]);
            string imageName = destination["image_url"].ToString();
            this.icon.sprite = Resources.Load<Sprite>(imageName);
            Button loadDestinationBtn = this.destinationBtn.GetComponent<Button>();
            loadDestinationBtn.onClick.AddListener(LoadDestinationScene);

        }
        

        public void onUpdateItem( int index ) 
        {
            StartCoroutine(LoadPlaces(index));
        }
    }
}