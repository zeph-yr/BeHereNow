using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using BeHereNow.Configuration;

namespace BeHereNow
{
    public partial class ScreenViewController : BSMLAutomaticViewController
    {
        // Works great with an image that is 674 x 626 px
        // Hack: Couldnt figure out how to shift the image in the container so used text to move it over kekeke

        public string opacity;

        // Make constructor to set initial conditions of opacity based on saved config
        // This constructor is actually not required if we didn't need to set initial values of anything
        public ScreenViewController()
        {
            if (PluginConfig.Instance.rank_enabled)
            {
                opacity = "#ffffffdd";
            }
            else
            {
                opacity = "";
            }
        }


        // value of opacity is directly changed by ModUI
        // then value of Rank is changed by ModUI, so that it will go and grab the (updated) value of opacity 
        [UIValue("rank")]
        public string Rank
        {
            get => opacity;
            set
            {
                NotifyPropertyChanged();
            }
        }


        [UIAction("#post-parse")]
        public void PostParse()
        {
            Rank = "changed";
        }
    }
}