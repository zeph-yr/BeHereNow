using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeHereNow.Configuration;

namespace BeHereNow
{
    public class ModUI : NotifiableSingleton<ModUI>
    {
        public ModUI()
        {
            // Must have this constructor or bsml wont load
        }


        [UIValue("enabled")]
        public bool Enabled
        {
            get => PluginConfig.Instance.enabled;
            set
            {
                PluginConfig.Instance.enabled = value;
            }
        }

        [UIAction("set_enabled")]
        void Set_Enabled(bool value)
        {
            Enabled = value;

            if (value)
            {
                ScreenController.Instance.Show();
            }
            else
            {
                ScreenController.Instance.Hide();
            }
        }


        [UIValue("rank_enabled")]
        public bool Rank_Enabled
        {
            get => PluginConfig.Instance.rank_enabled;
            set
            {
                PluginConfig.Instance.rank_enabled = value;
            }
        }

        [UIAction("set_rank_enabled")]
        void Set_Rank_Enabled(bool value)
        {
            Rank_Enabled = value;

            // Change opacity in ScreenViewController
            if (value == false)
            {
                // We purposely made screenViewController internal so we can access it here 
                ScreenController.Instance.screenViewController.opacity = "#ffffffdd";
                ScreenController.Instance.screenViewController.Rank = "changed";
            }
            else
            {
                ScreenController.Instance.screenViewController.opacity = "";
                ScreenController.Instance.screenViewController.Rank = "changed";
            }
        }
    }
}