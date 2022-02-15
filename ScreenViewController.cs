using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using BeHereNow.Configuration;

namespace BeHereNow
{
    public partial class ScreenViewController : BSMLAutomaticViewController
    {
        // Works great with an image that is 674 x 626 px
        public string opacity = "#ffffffdd";

        public string  Show_Rank()
        {
            return "#ffffffdd";
        }


        [UIValue("rank")]
        public string Rank
        {
            get => opacity;
            set
            {
                NotifyPropertyChanged();
            }
        }

        


        // Instead of shifting image, maybe change opacity of background instead, then gives option to not use a picture at all
        /*private string Show_Rank()
        {
            if (PluginConfig.Instance.rank_enabled)
            {
                return "#ffffffdd";
            }
            else
            {
                //return "--------";
                return "";
            }
        }*/

        [UIAction("#post-parse")]
        public void PostParse()
        {
            Rank = "changed";
        }
    }
}