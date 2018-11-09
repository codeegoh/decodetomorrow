using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamTexture : MonoBehaviour {
    
    private WebCamTexture camBG;
    public RawImage rawimage;
    public Texture textureBG;

    static public CamTexture instance;

    private void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
       

        camBG = new WebCamTexture(Screen.width, Screen.height);
        //rawimage.texture = camBG;

        rawimage.material.mainTexture = camBG;
        /*int rotAngle = -camBG.videoRotationAngle;
        while (rotAngle < 0) rotAngle += 360;
        while (rotAngle > 360) rotAngle -= 360;*/
        camBG.Play();
        rawimage.texture = camBG;
    }

    public void ToggleWebCam(bool isOn)
    {
        if (!isOn)
        {
            camBG.Stop();
            rawimage.texture = textureBG;
        }
        else{
            camBG = new WebCamTexture();
            rawimage.texture = camBG;
            rawimage.material.mainTexture = camBG;
            camBG.Play();
        }
           
        
    }


}
