using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using System.Linq;
using UnityEngine;

namespace BeHereNow
{
    internal class ScreenController
    {
        private FloatingScreen floatingScreen;
        private ScreenViewController screenViewController;


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


        public void Create_Screen()
        {
            Plugin.Log.Debug("Create_Screen()");

            if (floatingScreen == null)
            {
                // Make FloatingScreen:
                Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
                rotation = Quaternion.AngleAxis(55, Vector3.up);

                //Size (100,100) and position (0f, 1.05f, 1.95f) is good for a huge-to-the-floor panel where the player is standing lol
                floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(120, 90), false, new Vector3(3.4f, 1.30f, 1.95f), rotation);
                GameObject.DontDestroyOnLoad(floatingScreen.gameObject);

                // Make ViewControlller and give it to the FloatingScreen:
                screenViewController = BeatSaberUI.CreateViewController<ScreenViewController>();
                floatingScreen.SetRootViewController(screenViewController, HMUI.ViewController.AnimationType.None);

                floatingScreen.gameObject.SetActive(false);

                // Make FloatingScreen appear/disappear with leaderboard
                PlatformLeaderboardViewController platformLeaderboardViewController = Resources.FindObjectsOfTypeAll<PlatformLeaderboardViewController>().FirstOrDefault();
                platformLeaderboardViewController.didActivateEvent += PlatformLeaderboardViewController_didActivateEvent;
                platformLeaderboardViewController.didDeactivateEvent += PlatformLeaderboardViewController_didDeactivateEvent;

                Plugin.Log.Debug("Made screen");
            }
        }

        private void PlatformLeaderboardViewController_didActivateEvent(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            Plugin.Log.Debug("Screen active");
            floatingScreen.gameObject.SetActive(true);
        }

        private void PlatformLeaderboardViewController_didDeactivateEvent(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            Plugin.Log.Debug("Screen not active");
            floatingScreen.gameObject.SetActive(false);
        }
    }
}