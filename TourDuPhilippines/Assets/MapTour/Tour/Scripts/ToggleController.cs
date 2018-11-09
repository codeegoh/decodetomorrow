using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour 
{
	public  bool isOn;

	public Image toggleBgImage;
	public RectTransform toggle;

	public GameObject handle;
	private RectTransform handleTransform;

	private float handleSize;
	private float onPosX;
	private float offPosX;

	public float handleOffset;

	public GameObject onIcon;
	public GameObject offIcon;


	public float speed;
	static float t = 0.0f;

	private bool switching = false;
    //public DeviceCameraController camControl;


    private Color colorOn;
    private Color colorOff;

	void Awake()
	{
		handleTransform = handle.GetComponent<RectTransform>();
		RectTransform handleRect = handle.GetComponent<RectTransform>();
		handleSize = handleRect.sizeDelta.x;
		float toggleSizeX = toggle.sizeDelta.x;
		onPosX = (toggleSizeX / 2) - (handleSize/2) - handleOffset;
		offPosX = onPosX * -1;

	}


	void Start()
	{
		if(isOn)
		{
            if (ColorUtility.TryParseHtmlString("#53bcd4", out colorOn))
            {
                toggleBgImage.color = colorOn;//Color.green;
                handleTransform.localPosition = new Vector3(onPosX, 0f, 0f);
                onIcon.gameObject.SetActive(true);
                offIcon.gameObject.SetActive(false);
            }
		}
		else
		{
            if (ColorUtility.TryParseHtmlString("#cadde3", out colorOff))
            {
                toggleBgImage.color = colorOff;//Color.red;
                handleTransform.localPosition = new Vector3(offPosX, 0f, 0f);
                onIcon.gameObject.SetActive(false);
                offIcon.gameObject.SetActive(true);
            }
		}
	}
		
	void Update()
	{

		if(switching)
		{
			Toggle(isOn);
		}
	}

    public void StopPlayWebcam()
	{
		Debug.Log(isOn);
        //camControl.ToggleWebCam(isOn);
	}

	public void Switching()
	{
		switching = true;
	}
		


	public void Toggle(bool toggleStatus)
	{
		if(!onIcon.active || !offIcon.active)
		{
			onIcon.SetActive(true);
			offIcon.SetActive(true);
		}

		if(toggleStatus)
		{
            
            if (ColorUtility.TryParseHtmlString("#53bcd4", out colorOn)){
                toggleBgImage.color = colorOn;//SmoothColor(Color.green, Color.red);
                Transparency(onIcon, 1f, 0f);
                Transparency(offIcon, 0f, 1f);
                handleTransform.localPosition = SmoothMove(handle, onPosX, offPosX);
            }
           
		}
		else 
		{
            
            if (ColorUtility.TryParseHtmlString("#cadde3", out colorOff))
            {
                toggleBgImage.color = colorOff;//SmoothColor(Color.red, Color.green);
                Transparency(onIcon, 0f, 1f);
                Transparency(offIcon, 1f, 0f);
                handleTransform.localPosition = SmoothMove(handle, offPosX, onPosX);
            }
		}
			
	}


	Vector3 SmoothMove(GameObject toggleHandle, float startPosX, float endPosX)
	{
		
		Vector3 position = new Vector3 (Mathf.Lerp(startPosX, endPosX, t += speed * Time.deltaTime), 0f, 0f);
		StopSwitching();
		return position;
	}

	Color SmoothColor(Color startCol, Color endCol)
	{
		Color resultCol;
		resultCol = Color.Lerp(startCol, endCol, t += speed * Time.deltaTime);
		return resultCol;
	}

	CanvasGroup Transparency (GameObject alphaObj, float startAlpha, float endAlpha)
	{
		CanvasGroup alphaVal;
		alphaVal = alphaObj.gameObject.GetComponent<CanvasGroup>();
		alphaVal.alpha = Mathf.Lerp(startAlpha, endAlpha, t += speed * Time.deltaTime);
		return alphaVal;
	}

	void StopSwitching()
	{
		if(t > 1.0f)
		{
			switching = false;

			t = 0.0f;
			switch(isOn)
			{
			case true:
				isOn = false;
				StopPlayWebcam();
				break;

			case false:
				isOn = true;
				StopPlayWebcam();
				break;
			}

		}
	}

}
