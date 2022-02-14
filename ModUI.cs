using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeHereNow.Configuration;

namespace BeHereNow
{
    public class ModUI : NotifiableSingleton<ModUI>
    {
        public ModUI()
        {

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
        }
    }
}