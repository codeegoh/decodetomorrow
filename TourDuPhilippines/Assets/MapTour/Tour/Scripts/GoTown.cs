using System.Collections;
using System.Collections.Generic;
//using GoShared;
using UnityEngine;
using SimpleJSON;
using DigiShared;
namespace DigiMap
{
    public class GoTown : MonoBehaviour
    {

        public GOMap goMap;

        public string baseUrl = "https://onereward.gobeyondtheclouds.com/api/v1/maps/";
        public GameObject prefab;

        public int numCards = 1;
        //public GameObject card;
        public float spawnRadius = 200.0f;
        public float queryRadius = 5;

        private int minAngleObj = 0;
        private int maxAngleObj = 360;
        private float yDistanceToGround = 6.35f;
        private float minWaitTime = 1.0f;
        private float maxWaitTime = 10.0f;
        private bool loadOnce = false;
        Coordinates lastQueryCenter = null;
        private float elapsedTime;
        float TIME_OUT = 15.0f;
        private string responseToken;
        // Use this for initialization
        void Awake()
        {
            responseToken = "";
           
            //register this class for location notifications
            //goMap.locationManager.onOriginSet += LoadData;
            //goMap.locationManager.onLocationChanged += LoadData;
            goMap.OnTileLoad.AddListener((GOTile) => {
                OnLoadTile(GOTile);
            });
        }

        void OnLoadTile(GOTile tile)
        {
            string rad = "5";
            if(!loadOnce)
                StartCoroutine(queryPos(tile.goTile.tileCenter.longitude.ToString(), tile.goTile.tileCenter.latitude.ToString(), rad.ToString()/*, value => responseToken = value*/));

        }

       /* void LoadData(Coordinates currentLocation)
        {//This is called when the location changes
            string rad = "5";
            if (lastQueryCenter == null || lastQueryCenter.DistanceFromPoint(currentLocation) >= queryRadius / 1.5f)
            { //Do the request only if approaching the limit of the previous one
                lastQueryCenter = currentLocation;
                StartCoroutine(queryPos(currentLocation.longitude.ToString(), currentLocation.latitude.ToString(), rad.ToString(), value => responseToken = value));
            }
        }*/

        public Dictionary<string, string> headerCredential(WWWForm form)
        {
            Dictionary<string, string> headers = form.headers;
            headers["X-API-KEY"] = "b052d7bb032db7f181b93b85c0cddce5e493fbec";
            headers["Authorization"] = "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("android-staging-user:58cd52f9a2b59910017901fe7927efeb174db1a1"));
            headers["Content-Type"] = "application/x-www-form-urlencoded";
            Debug.Log("Authorization header : " + headers["Authorization"]);
            return headers;
        }
        public IEnumerator queryPos(string longPos, string latPos, string radiusQuery/*, System.Action<string> result*/)
        {
            //ApiCall apiCall = new ApiCall();
            WWWForm form = new WWWForm();
            form.AddField("long", longPos);
            form.AddField("lat", latPos);
            form.AddField("distance", int.Parse(radiusQuery));
            WWW www = new WWW(baseUrl + "nearest", form.data, headerCredential(form));
            Debug.Log("WWW login: " + baseUrl + "nearest");

            yield return www;

            Debug.Log("@@@@@ QUERY POS: " + www.text);
            //result(www.text);
            LoadPlaces(www.text);
            loadOnce = true;

        }


        Vector3 RandomCirclePos(Vector3 center, float radius)
        {
            Vector3 pos;
            pos.x = Random.insideUnitSphere.x * radius + center.x;
            pos.y = yDistanceToGround;
            pos.z = Random.insideUnitSphere.z * radius + center.z;
            Debug.Log("POSITION: " + pos);
            return pos;
        }

        public void LoadPlaces(string townData)
        { //Request the API

            JSONNode array = JSONArray.Parse(townData);
            double lat = 0;
            double lng = 0;
            string owner = "";
            Debug.Log("TOWN Load Places: " + array.Count);
            for (int i = 0; i < array.Count; i++)
            {
                lat = array[i]["latitude"].AsDouble;
                lng = array[i]["longitude"].AsDouble;
                owner = array[i]["town_owner"].ToString();
                Debug.Log ("OWNER: "+owner);
                Coordinates coordinates = new Coordinates(lat, lng, 0);
                GameObject go = GameObject.Instantiate(prefab);
                go.transform.localPosition = coordinates.convertCoordinateToVector(0);
                go.transform.parent = transform;
                /*if (owner.Equals("false"))
                {
                    go.GetComponent<PokeStop>().townDetail.text = "NONE";
                }
                else
                {
                    go.GetComponent<PokeStop>().townDetail.text = owner;
                }*/

            }
        }

       /* IEnumerator randomSpawnTime(GameObject town)
        {
            Vector3 center = town.transform.position;
            for (int i = 0; i < numCards; i++)
            {
                Vector3 pos = RandomCirclePos(center, spawnRadius);
                float yRotate = Random.Range(minAngleObj, maxAngleObj);
                Instantiate(card, pos, Quaternion.Euler(0.0f, yRotate, 0.0f));
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            }
        }*/

    }
}
