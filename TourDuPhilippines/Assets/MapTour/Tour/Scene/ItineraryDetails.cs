namespace DigiFrame
{

    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class ItineraryDetails : UIBehaviour, IDynamicScrollViewItem
    {

        private readonly Color[] colors = new Color[] {
            Color.cyan,
            Color.green,
        };

        string[] roadtrip = new string[]{
            "EL NIDO PACKUP",
            "TAYTAY ESCAPADE",
            "CORON ACTIVITIES",
            "NARRA EXPLORATION",
            "FOOD TRIP"
        };

        public Text details;


        public void onUpdateItem(int index)
        {
            this.details.text = roadtrip[index];
        }
    }
}