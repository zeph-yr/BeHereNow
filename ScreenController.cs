using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using BeHereNow.Configuration;
using System;
using System.Linq;
using UnityEngine;

namespace BeHereNow
{
    internal class ScreenController
    {
        internal FloatingScreen floatingScreen;
        internal ScreenViewController screenViewController;

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
                Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
                rotation = Quaternion.AngleAxis(55, Vector3.up);

                //Size (100,100) and position (0f, 1.05f, 1.95f) is good for a huge-to-the-floor panel where the player is standing lol
                floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(120, 90), false, new Vector3(3.45f, 1.30f, 2.1f), rotation);
                GameObject.DontDestroyOnLoad(floatingScreen.gameObject);

                screenViewController = BeatSaberUI.CreateViewController<ScreenViewController>();
                floatingScreen.SetRootViewController(screenViewController, HMUI.ViewController.AnimationType.None);

                floatingScreen.gameObject.SetActive(false);

                PlatformLeaderboardViewController platformLeaderboardViewController = Resources.FindObjectsOfTypeAll<PlatformLeaderboardViewController>().FirstOrDefault();
                platformLeaderboardViewController.didActivateEvent += PlatformLeaderboardViewController_didActivateEvent;
                platformLeaderboardViewController.didDeactivateEvent += PlatformLeaderboardViewController_didDeactivateEvent;

                LocalLeaderboardViewController localLeaderboardViewController = Resources.FindObjectsOfTypeAll<LocalLeaderboardViewController>().FirstOrDefault();
                localLeaderboardViewController.didActivateEvent += LocalLeaderboardViewController_didActivateEvent;
                localLeaderboardViewController.didDeactivateEvent += LocalLeaderboardViewController_didDeactivateEvent;

                //Plugin.Log.Debug("Made screen");
            }
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