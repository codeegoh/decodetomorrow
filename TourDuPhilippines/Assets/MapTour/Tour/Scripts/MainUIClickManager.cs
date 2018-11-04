using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIClickManager : MonoBehaviour
{

    static public MainUIClickManager instance;
        public Button mapBtn;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            Button mapBtnClick = mapBtn.GetComponent<Button>();
            mapBtnClick.onClick.AddListener(LoadMapScene);
        }


        void LoadMapScene()
        {
            SceneManager.LoadScene("Map");
        }
    }

