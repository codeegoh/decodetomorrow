namespace DigiFrame
{

    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class ItineraryItem : UIBehaviour, IDynamicScrollViewItem
    {
    
        public Image icon;
        public Button iconBtn;
        public GameObject panelDetails;
        
        private bool isDone = false;



        public IEnumerator LoadPlaces(int index)
        { //Request the API

            ParseJob job = new ParseJob();
            job.InData = Resources.Load<TextAsset>("itinerary").text;
            job.Start();

            yield return StartCoroutine(job.WaitFor());

            IDictionary response = (IDictionary)((IDictionary)job.OutData);
            IList results = (IList)response["results"];
            IDictionary destination = ((IDictionary)results[index]);
            string imageName = destination["image_url"].ToString();
            this.icon.sprite = Resources.Load<Sprite>(imageName);
            Button iconBtnClicked = iconBtn.GetComponent<Button>();
            iconBtnClicked.onClick.AddListener(ShowDetails);
        }

        void ShowDetails()
        {
            panelDetails.SetActive(true);
        }
        public void onUpdateItem(int index)
        {
            StartCoroutine(LoadPlaces(index));
        }
    }
}