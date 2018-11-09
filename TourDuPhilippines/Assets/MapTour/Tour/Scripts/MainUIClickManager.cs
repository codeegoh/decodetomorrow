using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIClickManager : MonoBehaviour
{

    static public MainUIClickManager instance;
    public Button mapBtn, checkIn;
    public GameObject panelAddTour, panelLogin, panelGallery;
    //public Button privacyPolicy;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("AddTravel"))
        {
            if (PlayerPrefs.GetInt("AddTravel") == 1)
            {
                panelAddTour.SetActive(true);
                panelLogin.SetActive(false);
                PlayerPrefs.SetInt("AddTravel", 0);
            }
        }

        if (PlayerPrefs.HasKey("AddGallery"))
        {
            if (PlayerPrefs.GetInt("AddGallery") == 1)
            {
                panelGallery.SetActive(true);
                panelLogin.SetActive(false);
                PlayerPrefs.SetInt("AddGallery", 0);
            }
        }
        if(PlayerPrefs.HasKey("FBSuccess"))
        {
            if(PlayerPrefs.GetInt("FBSuccess")==1)
            {
                panelLogin.SetActive(false);
            }
        }


        Button mapBtnClick = mapBtn.GetComponent<Button>();
        mapBtnClick.onClick.AddListener(LoadMapScene);

        Button checkInClicked = checkIn.GetComponent<Button>();
        checkInClicked.onClick.AddListener(LoadCheckInScene);
    }


    void LoadMapScene()
    {
         SceneManager.LoadScene("Map");
    }
    void LoadCheckInScene()
    {
        SceneManager.LoadScene("TourCheckIn");
    }
}

