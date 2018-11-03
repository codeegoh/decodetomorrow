namespace DigiFrame {

    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class Item : UIBehaviour, IDynamicScrollViewItem 
    {
	    private readonly Color[] colors = new Color[] {
		    Color.cyan,
		    Color.green,
	    };

        public Image icon;
	    public Text  title;
	    public Image background;
        private List <string> tempPic = new List<string> ();
        private List<string> tempImg = new List<string>();
        private bool isDone = false;


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
                isDone = true;

            }
    }
        IEnumerator DelayThis()
        {
            yield return StartCoroutine(LoadPlaces());
        }

        public void onUpdateItem( int index ) {
            StartCoroutine(DelayThis());
            if (isDone)
            {
                this.title.text = tempImg[index];
                this.icon.sprite = Resources.Load<Sprite>(tempPic[index]);
            }
        }
    }
}