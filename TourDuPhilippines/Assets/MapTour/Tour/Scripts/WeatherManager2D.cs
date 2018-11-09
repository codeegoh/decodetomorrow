using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Digi.RainMaker;
using Digify;


public class WeatherManager2D : MonoBehaviour 
{

    static public WeatherManager2D instance;
    public string baseUrl = "https://api-dev.weathersolutions.ph/api/v1/forecast/";
    public string oauth_token = "Token cc84d57f4084333434f1068cde634ea6b7d20fa4";
    private ForecastDataStore forecastDataStore;
    private Dictionary<string, List<Forecast>> forecastData;
    public Text choosenDate, temperatureText, radiationText,
    cloudCoverText, rainText, recommendationText;

    
    [HideInInspector]
    public float userLatitude, userLongitude;
    [HideInInspector]
    public bool canAccessWeather = false;
    private bool isWeatherAvailable = false;
    public RainScript2D RainScript;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
//#if UNITY_EDITOR

        userLatitude = 20.4549408F;
        userLongitude = 121.9598284F;
        string url = baseUrl + userLatitude + "," + userLongitude + "/?";

        string key = "location_name";
        StartCoroutine(LoadForecast(url, key));
        Debug.Log("My Location is at Batanes");
//#elif UNITY_IOS || UNITY_ANDROID
//        StartCoroutine(AccessUserLocation());
//#endif

        forecastData = new Dictionary<string, List<Forecast>>();

    }
    public void UpdateWeatherTextInfo(int index)
    {
        List<Forecast> forecasts = GetForecast("location_name");
        if (null != forecasts && forecasts.Count > 0)
        {
            choosenDate.text = forecasts[index].Timestamp.ToString();
            temperatureText.text = forecasts[index].Temperature.ToString();
            radiationText.text = forecasts[index].SolarRadiation.ToString();
            cloudCoverText.text = forecasts[index].TotalCloudCover.ToString();
            RainScript.RainIntensity = (float)forecasts[index].Rain/20;
            string weatherInterpretationRain = WeatherInterpretationFactory.Interpret(forecasts[index], WeatherType.RAIN);
            rainText.text = weatherInterpretationRain;
        }
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
            //Location location = new Location(userLatitude, userLongitude, 0);
            StartCoroutine(LoadForecast(url, "location_name"));
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }
        Input.location.Stop();
    }

    public IEnumerator LoadForecast(string url, string locationKey)
    {
        Debug.Log("WEATHER API URL: " + url);
        Hashtable headers = new Hashtable();
        headers.Add("Authorization", oauth_token);
        WWW www = new WWW(url, null, headers);
        yield return www;


        ParseJob job = new ParseJob();
        job.InData = www.text;
        job.Start();
        Debug.Log("#####WEATHER Response" + www.text);
        yield return StartCoroutine(job.WaitFor());

        IDictionary response = (IDictionary)((IDictionary)job.OutData);
        IList results = (IList)response["results"];

        forecastDataStore = (ForecastDataStore)new DataStoreFactory().Create(StoreType.FORECAST);

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

                forecastDataStore.Add(forecast);
            }
        }

        forecastData.Add(locationKey, forecastDataStore.All());
        isWeatherAvailable = true;
        canAccessWeather = true;
        PlayerPrefs.SetInt("IsDoneText", 1);
    }

    public List<Forecast> GetForecast(string locationKey)
    {
        return forecastData[locationKey];
    }

    private void Update()
    {
        if (isWeatherAvailable)
        {
            Location location = new Location(userLatitude, userLongitude, 0);
            List<Forecast> forecasts = GetForecast("location_name");
            if (null != forecasts && forecasts.Count > 0)
            {
                foreach (Forecast forecast in forecasts)
                {

                    //Debug.Log("@@@@@Rain Probability: " + forecast.RainProbability.ToString());
                    //Debug.Log("@@@@@Rain GAILE: " + forecast.Rain.ToString());
                    RainScript.RainIntensity = (float)forecast.Rain / 20;
                }
                isWeatherAvailable = false;
            }
        }
    }

}
