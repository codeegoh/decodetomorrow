using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TourClickManager : MonoBehaviour {

    public Button backToMain;

    void Start()
    {
        Button backToMainClick = backToMain.GetComponent<Button>();
        backToMainClick.onClick.AddListener(LoadMapScene);
    }

    void LoadMapScene()
    {
        SceneManager.LoadScene("Map");
    }
}
