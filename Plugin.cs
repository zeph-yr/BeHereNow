using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPALogger = IPA.Logging.Logger;

namespace BeHereNow
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }


        [Init]
        public Plugin(IPALogger logger, Config conf)
        {
            Instance = this;
            Plugin.Log = logger;
            Plugin.Log?.Debug("Logger initialized.");

            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Plugin.Log?.Debug("Config loaded");
        }


        [OnEnable]
        public void OnEnable()
        {
            BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("BeHereNow", "BeHereNow.ModUI.bsml", ModUI.instance, BeatSaberMarkupLanguage.GameplaySetup.MenuType.Solo);

            BS_Utils.Utilities.BSEvents.lateMenuSceneLoadedFresh += BSEvents_lateMenuSceneLoadedFresh;
            BS_Utils.Utilities.BSEvents.gameSceneLoaded += BSEvents_gameSceneLoaded;
        }

        private void BSEvents_gameSceneLoaded()
        {
            ScreenController.Instance.floatingScreen.gameObject.SetActive(false); //Not sure why sometimes it shows up in the map lol
        }

        private void BSEvents_lateMenuSceneLoadedFresh(ScenesTransitionSetupDataSO obj)
        {
            ScreenController.Instance.Create_Screen();
        }


        [OnDisable]
        public void OnDisable()
        {
            BS_Utils.Utilities.BSEvents.lateMenuSceneLoadedFresh -= BSEvents_lateMenuSceneLoadedFresh;
        }
    }
}
