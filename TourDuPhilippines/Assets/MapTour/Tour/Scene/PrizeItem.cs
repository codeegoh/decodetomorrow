namespace DigiFrame
{

    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class PrizeItem : UIBehaviour, IDynamicScrollViewItem
    {


        public Image productIcon, sponsorIcon;
        public Text description, validity;

        private List<string> tempPic = new List<string>();
        private List<string> tempImg = new List<string>();
        private List<string> tempSponsor = new List<string>();
        private List<string> tempValidity = new List<string>();
        private bool isDone = false;


        public IEnumerator LoadPrizes(int index)
        { //Request the API

            ParseJob job = new ParseJob();
            job.InData = Resources.Load<TextAsset>("prize").text;
            job.Start();

            yield return StartCoroutine(job.WaitFor());

            IDictionary response = (IDictionary)((IDictionary)job.OutData);
            IList results = (IList)response["results"];
            IDictionary destination = ((IDictionary)results[index]);
            string imagePic = destination["product_image_url"].ToString();
            string imageName = destination["description"].ToString();
            string sponsor = destination["sponsor_image_url"].ToString();
            string valid = destination["validity"].ToString();

            this.description.text = imageName;
            this.productIcon.sprite = Resources.Load<Sprite>(imagePic);
            this.sponsorIcon.sprite = Resources.Load<Sprite>(sponsor);
            this.validity.text = valid;

        }


        public void onUpdateItem(int index)
        {
            StartCoroutine(LoadPrizes(index));

        }
    }
}