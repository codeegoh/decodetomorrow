using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AnimatePrefab : MonoBehaviour
{

    public float speed = 50;
    public bool continousRotation;
    bool isAnimating;


    void OnCollisionEnter(Collision collision)
    {

        if (!isAnimating && !continousRotation)
            StartCoroutine(rotate(1));
        else if (continousRotation)
            transform.Rotate(transform.eulerAngles.x, speed * Time.deltaTime, transform.eulerAngles.z);
    }

    private void OnCollisionStay(Collision collision)
    {
#if UNITY_EDITOR
        Debug.Log("Collided");
        if (Input.GetMouseButton(0))
        {
            CastRay();
        }
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            Debug.Log("Collided");

            CastRay();
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

    private IEnumerator rotate(float time) {

		isAnimating = true;
		float elapsedTime = 0;

		while (elapsedTime < time)
		{
			float value = Mathf.Lerp (0, 360, elapsedTime);
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, value, transform.eulerAngles.z);
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		isAnimating = false;
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 0, transform.eulerAngles.z);

	}

}
