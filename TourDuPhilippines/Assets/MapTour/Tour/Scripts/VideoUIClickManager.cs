using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoUIClickManager : MonoBehaviour {

    public Button backToMain, addTravelPlanBtn, gotoGallery;

    void Start()
    {
        Button backToMainClick = backToMain.GetComponent<Button>();
        backToMainClick.onClick.AddListener(LoadMainScene);

        Button addTravelClicked = addTravelPlanBtn.GetComponent<Button>();
        addTravelClicked.onClick.AddListener(() => LoadTravelPlan("AddTravel"));

        Button galleryClicked = gotoGallery.GetComponent<Button>();
        gotoGallery.onClick.AddListener(() => LoadTravelPlan("AddGallery"));
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    void LoadTravelPlan(string prefs)
    {

        PlayerPrefs.SetInt(prefs, 1);
        SceneManager.LoadScene("Main");
    }
}
