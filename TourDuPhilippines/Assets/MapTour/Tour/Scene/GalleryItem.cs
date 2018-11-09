namespace DigiFrame
{

    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class GalleryItem : UIBehaviour, IDynamicScrollViewItem
    {
    

        public Image icon1, icon2;
        private bool isDone = false;


        /*void LoadDestinationScene()
        {
            SceneManager.LoadScene("DestinationVideo");
        }*/
        public IEnumerator LoadPlaces(int index)
        { //Request the API

            ParseJob job = new ParseJob();
            job.InData = Resources.Load<TextAsset>("gallery").text;
            job.Start();

            yield return StartCoroutine(job.WaitFor());

            IDictionary response = (IDictionary)((IDictionary)job.OutData);
            IList results = (IList)response["results"];
            IDictionary destination = ((IDictionary)results[index]);
            string nameIcon = destination["icon1"].ToString();
            this.icon1.sprite = Resources.Load<Sprite>(nameIcon);
            string nameIcon2 = destination["icon2"].ToString();
            this.icon2.sprite = Resources.Load<Sprite>(nameIcon2);

        }

        public void onUpdateItem(int index)
        {
            StartCoroutine(LoadPlaces(index));

        }
    }
}