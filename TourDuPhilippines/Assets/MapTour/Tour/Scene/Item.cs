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
	    public Text  title;
	    //public Image background;
        public Button destinationBtn;
        private List <string> tempPic = new List<string> ();
        private List<string> tempImg = new List<string>();
        private bool isDone = false;

        void Start()
        {
            StartCoroutine(LoadPlaces());
        }

        void LoadDestinationScene()
        {
            SceneManager.LoadScene("DestinationVideo");
        }
        public IEnumerator LoadPlaces()
        { //Request the API

            ParseJob job = new ParseJob();
            job.InData = Resources.Load<TextAsset>("destination").text;
            job.Start();

            yield return StartCoroutine(job.WaitFor());

            IDictionary response = (IDictionary)((IDictionary)job.OutData);
            IList results = (IList)response["results"];
            foreach (IDictionary result in results)
            {
                IDictionary destination = ((IDictionary)result);
                string imagePic = destination["image_url"].ToString();
                string imageName = destination["destinationName"].ToString();
                tempImg.Add(imageName);
                tempPic.Add(imagePic);
                Debug.Log("Image Count"+ tempImg.Count +"Results Count"+results.Count);
                if(tempImg.Count == results.Count)
                {
                    isDone = true;
                }


             }

        }
        

        public void onUpdateItem( int index ) {

            if (isDone )
            {
                this.title.text = tempImg[index];
                this.icon.sprite = Resources.Load<Sprite>(tempPic[index]);
                Button loadDestinationBtn = this.destinationBtn.GetComponent<Button>();
                loadDestinationBtn.onClick.AddListener(LoadDestinationScene);
            }
        }
    }
}