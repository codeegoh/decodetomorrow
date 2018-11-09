using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlatPOI : MonoBehaviour {

    //public TextMesh textMesh;
    public float offsetXAngle = 75;
    int minimumFontSize = 35;
    public bool adaptSize = true;

    public void Start()
    {
        //gaile: I added this because the POI is too low.
        Vector3 posObj = transform.localPosition;
        posObj.y = 10;//-495;
        transform.localPosition = posObj;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        Vector3 p = transform.eulerAngles;
        p.x += 90;
        transform.eulerAngles = p;
    }
    void OnCollisionStay(Collision collision)
    {
#if UNITY_EDITOR
        Debug.Log("Collided");
        if (Input.GetMouseButton(0))
        {
            CastRay();
            MapUIClickManager.instance.prize.SetActive(true);
        }
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            Debug.Log("Collided");

            CastRay();
            MapUIClickManager.instance.prize.SetActive(true);
        }
#endif
    }

    void CastRay()
    {
#if UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("Town") /*&& hit.collider != null*/)
            {
                Debug.Log("$$$$$$ TAPPED POI");
                SceneManager.LoadScene("TourCheckIn");
                //TODO: Gaile comment this for now, so we could use the same POI
                //hit.collider.enabled = false;
            }
        }
#elif UNITY_IOS || UNITY_ANDROID
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("Town") /*&& hit.collider != null*/)
            {
                Debug.Log("$$$$$$ TAPPED POI");
                SceneManager.LoadScene("TourCheckIn");
                //TODO: Gaile comment this for now, so we could use the same POI
                //hit.collider.enabled = false;
            }
        }
#endif
    }
}
