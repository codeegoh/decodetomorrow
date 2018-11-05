using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Digify;
using System;

public class WeatherManager : MonoBehaviour
{

    public string baseUrl = "https://api-dev.weathersolutions.ph/api/v1/forecast/";
    public string oauth_token = "Token cc84d57f4084333434f1068cde634ea6b7d20fa4";
    private ForecastDataStore forecastData;
    [HideInInspector]
    public float userLatitude, userLongitude;

    void Start()
    {
#if UNITY_EDITOR
        userLatitude = 14.6337F;
        userLongitude = 121.0413113F;
        string url = baseUrl+userLatitude+","+userLongitude+"/?";
        StartCoroutine(LoadForecast(url));
        Debug.Log("My Location is at GMA");
#elif UNITY_IOS || UNITY_ANDROID
        StartCoroutine(AccessUserLocation());
#endif

        forecastData = (ForecastDataStore) new DataStoreFactory().Create(StoreType.FORECAST);
    }

    IEnumerator AccessUserLocation()
    {
        if (!Input.location.isEnabledByUser)
            yield break;
            
        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            userLatitude = Input.location.lastData.latitude;
            userLongitude = Input.location.lastData.longitude;
            string url = baseUrl + userLatitude + "," + userLongitude + "/?";
            StartCoroutine(LoadForecast(url));
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }
        Input.location.Stop();
    }

    public IEnumerator LoadForecast(string url)
    {
        Debug.Log("WEATHER API URL: " + url);
        Hashtable headers = new Hashtable();
        headers.Add("Authorization", oauth_token);
        WWW www = new WWW(url, null, headers);
        yield return www;


        ParseJob job = new ParseJob();
        job.InData = www.text;
        job.Start();

        yield return StartCoroutine(job.WaitFor());

        IDictionary response = (IDictionary)((IDictionary)job.OutData);
        IList results = (IList)response["results"];

        foreach (IDictionary result in results)
        {//This example only takes GPS location and the name of the object. There's lot more, take a look at the Foursquare API documentation

            IDictionary currentData = ((IDictionary)result);
            string timeStamp = currentData["timestamp"].ToString();
            DateTime enteredDate = DateTime.Parse(timeStamp);
            double temperature = double.Parse(currentData["temperature"].ToString());
            double windSpeed = double.Parse(currentData["wind_speed"].ToString());
            double solarRadiation = double.Parse(currentData["solar_radiation"].ToString());
            double meanSeaLevelPressure = double.Parse(currentData["mean_sea_level_pressure"].ToString());
            double rain = double.Parse(currentData["rain"].ToString());
            double dewpoint = double.Parse(currentData["dewpoint"].ToString());
            double windGust = double.Parse(currentData["wind_gust"].ToString());
            double windDirection = double.Parse(currentData["wind_direction"].ToString());
            double heatIndex = double.Parse(currentData["heat_index"].ToString());
            double totalCloudCover = double.Parse(currentData["total_cloud_cover"].ToString());
            double rainProbability = double.Parse(currentData["rain_probability"].ToString());
            if (timeStamp != null)
            {
                Debug.Log("GAILE Current Data:" + enteredDate + " " + temperature);
                Forecast forecast = new Forecast(enteredDate, temperature, windSpeed,
                                                 solarRadiation, meanSeaLevelPressure,
                                                 rain, dewpoint, windGust, windDirection, heatIndex,
                                                 totalCloudCover, rainProbability);

                forecastData.Add(forecast);
               
            }
        }
    }

}
