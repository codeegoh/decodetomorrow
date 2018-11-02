using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigiShared;
namespace DigiMap
{
    public class LoadWeather : MonoBehaviour
    {

        public GOMap goMap;
        public string baseUrl = "https://api-dev.weathersolutions.ph/api/v1/forecast/";
        public string oauth_token = "Token cc84d57f4084333434f1068cde634ea6b7d20fa4";
        public float queryRadius = 1000;

        Coordinates lastQueryCanter = null;

        void Awake(){
            goMap.OnTileLoad.AddListener((GOTile) =>
            {
                OnLoadTile(GOTile);
            });
        }

        void OnLoadTile (GOTile tile)
        {
            //Center of the map tile
            Coordinates tileCenter = tile.goTile.tileCenter;
            string url = baseUrl +tile.goTile.tileCenter.latitude+","+tile.goTile.tileCenter.longitude+"/";
            StartCoroutine(LoadPlaces(url));
        }

        public IEnumerator LoadPlaces(string url)
        {
            Debug.Log("WEATHER API URL: " + url);
            Hashtable headers = new Hashtable();
            headers.Add("Authorization", oauth_token);
            WWW www = new WWW(url,null, headers);
            yield return www;


            ParseJob job = new ParseJob();
            job.InData = www.text;
            job.Start();

            yield return StartCoroutine(job.WaitFor());

            IDictionary response = (IDictionary)((IDictionary)job.OutData);
            IList results = (IList)response["results"];



            foreach (IDictionary result in results)
            {//This example only takes GPS location and the name of the object. There's lot more, take a look at the Foursquare API documentation

                IDictionary currentData= ((IDictionary)result);
                string timeStamp = currentData["timestamp"].ToString();
                string temperature = currentData["temperature"].ToString();
                if(timeStamp !=null)
                    Debug.Log("GAILE Current Data:" +timeStamp+" " +temperature);


            }
        }
    }

}
