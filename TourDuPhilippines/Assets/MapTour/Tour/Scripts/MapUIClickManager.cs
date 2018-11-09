using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapUIClickManager : MonoBehaviour {

    static public MapUIClickManager instance;

    public Button backToMain;
    //public Button shortcutToGreet;

    public GameObject prize;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Button backToMainClick = backToMain.GetComponent<Button>();
        backToMainClick.onClick.AddListener(LoadMainScene);

       // Button shortcutToGreetClick = shortcutToGreet.GetComponent<Button>();
       // shortcutToGreetClick.onClick.AddListener(LoadGreetScene);
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    void LoadGreetScene()
    {
        SceneManager.LoadScene("TourCheckIn");
    }


}
