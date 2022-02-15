using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using BeHereNow.Configuration;
using System.Linq;
using UnityEngine;

namespace BeHereNow
{
    internal class ScreenController
    {
        // Must set these to internal so their components can be accessed by ModUI
        internal FloatingScreen floatingScreen;
        internal ScreenViewController screenViewController;


        // Constructing it this way and calling ScreenController.Instance.Create_Screen() is a must.
        // Making the FloatingScreen and ViewController static and calling a static Create_Screen() doesnt work at all
        public static ScreenController _instance { get; private set; }
        public static ScreenController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ScreenController();
                return _instance;
            }
        }

        internal void Create_Screen()
        {
            //Plugin.Log.Debug("Create_Screen()");

            if (floatingScreen == null)
            {
                // Make a FloatingScreen
                Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
                rotation = Quaternion.AngleAxis(55, Vector3.up);

                //Note: Size (100,100) and position (0f, 1.05f, 1.95f) is good for a huge-to-the-floor panel where the player is standing lol
                floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(120, 90), false, new Vector3(3.45f, 1.30f, 2.1f), rotation);
                GameObject.DontDestroyOnLoad(floatingScreen.gameObject);


                // Then make a ViewController and set the FloatingScreen's ViewController to it
                screenViewController = BeatSaberUI.CreateViewController<ScreenViewController>();
                floatingScreen.SetRootViewController(screenViewController, HMUI.ViewController.AnimationType.None);

                floatingScreen.gameObject.SetActive(false);


                PlatformLeaderboardViewController platformLeaderboardViewController = Resources.FindObjectsOfTypeAll<PlatformLeaderboardViewController>().FirstOrDefault();
                platformLeaderboardViewController.didActivateEvent += PlatformLeaderboardViewController_didActivateEvent;
                platformLeaderboardViewController.didDeactivateEvent += PlatformLeaderboardViewController_didDeactivateEvent;


                // Extra Bonus: We need to attach it to party leaderboard too otherwise if user opens party mode, it can get stuck in the lobby and looks weird
                LocalLeaderboardViewController localLeaderboardViewController = Resources.FindObjectsOfTypeAll<LocalLeaderboardViewController>().FirstOrDefault();
                localLeaderboardViewController.didActivateEvent += LocalLeaderboardViewController_didActivateEvent;
                localLeaderboardViewController.didDeactivateEvent += LocalLeaderboardViewController_didDeactivateEvent;

                //Plugin.Log.Debug("Made screen");
            }
        }


        private void PlatformLeaderboardViewController_didActivateEvent(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            //Plugin.Log.Debug("Screen active");

            if (PluginConfig.Instance.enabled)
            {
                floatingScreen.gameObject.SetActive(true);
            }
        }

        private void PlatformLeaderboardViewController_didDeactivateEvent(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            //Plugin.Log.Debug("Screen not active");

            floatingScreen.gameObject.SetActive(false);
        }


        private void LocalLeaderboardViewController_didActivateEvent(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (PluginConfig.Instance.enabled)
            {
                floatingScreen.gameObject.SetActive(true);
            }
        }

        private void LocalLeaderboardViewController_didDeactivateEvent(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            floatingScreen.gameObject.SetActive(false);
        }


        // These are called by ModUI by the Enable toggle
        internal void Show()
        {
            floatingScreen.gameObject.SetActive(true);
        }

        internal void Hide()
        {
            floatingScreen.gameObject.SetActive(false);
        }
    }
}