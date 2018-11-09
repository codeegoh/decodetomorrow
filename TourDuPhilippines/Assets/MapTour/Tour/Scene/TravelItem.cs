namespace DigiFrame
{

    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class TravelItem : UIBehaviour, IDynamicScrollViewItem
    {

        private readonly Color[] colors = new Color[] {
            Color.cyan,
            Color.green,
        };

        string[] roadtrip = new string[]{
            "plan_item_1",
            "plan_item_2",
            "plan_item_3"
        };

        public Image travelImg;


        public void onUpdateItem(int index)
        {
            this.travelImg.sprite = Resources.Load<Sprite>(roadtrip[index]);
        }
    }
}