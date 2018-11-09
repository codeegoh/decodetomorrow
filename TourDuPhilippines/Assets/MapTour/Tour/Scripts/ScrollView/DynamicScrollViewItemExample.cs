
namespace DigiFrame {

    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using Digify;
    using System.Collections;
    using System.Collections.Generic;

    public class DynamicScrollViewItemExample : UIBehaviour, IDynamicScrollViewItem 
    {
        //Color blue1 = new Color(255, 255, 255);
        private readonly Color[] colors = new Color[] {
            new Color(142, 225, 252),
            new Color(214, 246, 251)
	    };

	    public Text  title;
	    public Image background;
        public Button weatherDate;
        [HideInInspector]


        void LoadWeatherDetailsByDate(int weatherIndex)
        {
            List<Forecast> forecasts = WeatherManager2D.instance.GetForecast("location_name");
            if (null != forecasts && forecasts.Count > 0)
            {
                Debug.Log("&&&&& DATE TIME " + forecasts[weatherIndex].Timestamp);
                Debug.Log("&&&&&&& Rain Probability: " + forecasts[weatherIndex].RainProbability.ToString());
                Debug.Log("&&&&&&& Rain GAILE: " + forecasts[weatherIndex].Rain.ToString());
            }

        }

        public void onUpdateItem( int index ) {
            if (WeatherManager2D.instance.canAccessWeather)
            {
                List<Forecast> forecasts = WeatherManager2D.instance.GetForecast("location_name");
                if (null != forecasts && forecasts.Count > 0)
                {
                    this.title.text = forecasts[index].Timestamp.ToString();//string.Format("Name{0:d3}", (index + 1));
                    //this.background.color = this.colors[Mathf.Abs(index) % this.colors.Length];
                    Button weatherDateClick = this.weatherDate.GetComponent<Button>();

                    weatherDateClick.onClick.AddListener(() => WeatherManager2D.instance.UpdateWeatherTextInfo(index));
                }
            }
        }
    }
}