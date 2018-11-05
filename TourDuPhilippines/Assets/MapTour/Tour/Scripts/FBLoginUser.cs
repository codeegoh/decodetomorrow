
namespace Facebook.Unity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;


    public class FBLoginUser : MonoBehaviour
    {
        public Button fbBtn;
        public GameObject PanelMainContent;
        public GameObject PanelLogin;

        void Awake()
        {
            if (!FB.IsInitialized)
            {
                // Initialize the Facebook SDK
                FB.Init(InitCallback, OnHideUnity);
            }
            else
            {
                // Already initialized, signal an app activation App Event
                FB.ActivateApp();
            }
        }

        void Start()
        {
            Button fbBtnClick = fbBtn.GetComponent<Button>();
            fbBtnClick.onClick.AddListener(InitAndLogin);
        }
       

        void InitAndLogin()
        {
            if(FB.IsInitialized)
            {
                this.CallFBLogin();
            }else{
                FB.Init(this.OnInitComplete, this.OnHideUnity);
                //this.Status = "FB.Init() called with " + FB.AppId;
            }

        }



        private void CallFBLogin()
        {
            var perms = new List<string>() { "public_profile", "email" };
            FB.LogInWithReadPermissions(perms, ShowMainContent);

            //FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email"/*, "user_friends"*/ }, ShowMainContent);

        }

        private void CallFBLoginForPublish()
        {
            // It is generally good behavior to split asking for read and publish
            // permissions rather than ask for them all at once.
            //
            // In your own game, consider postponing this call until the moment
            // you actually need it.
            FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" }, ShowMainContent);
        }

        private void CallFBLogout()
        {
            FB.LogOut();
        }

       
        void ShowMainContent(ILoginResult result)
        {
            if (FB.IsLoggedIn)
            {
                // AccessToken class will have session details
                var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
                // Print current access token's User ID
                Debug.Log(aToken.UserId);
                // Print current access token's granted permissions
                foreach (string perm in aToken.Permissions)
                {
                    Debug.Log(perm);
                }
                PanelLogin.SetActive(false);
                PanelMainContent.SetActive(true);
            }
            else
            {
                Debug.Log("User cancelled login");

            }

        }
        private void OnInitComplete()
        {

        }

        private void OnHideUnity(bool isGameShown)
        {
            if (!isGameShown)
            {
                // Pause the game - we will need to hide
                Time.timeScale = 0;
            }
            else
            {
                // Resume the game - we're getting focus again
                Time.timeScale = 1;
            }
        }
        private void InitCallback()
        {
            if (FB.IsInitialized)
            {
                // Signal an app activation App Event
                FB.ActivateApp();
                // Continue with Facebook SDK
                // ...
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }


    }
}
