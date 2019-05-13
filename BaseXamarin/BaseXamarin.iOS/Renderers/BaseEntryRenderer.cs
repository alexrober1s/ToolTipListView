using BaseXamarin.Custom;
using BaseXamarin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BaseEntry), typeof(BaseEntryRenderer))]
namespace BaseXamarin.iOS.Renderers
{
    public class BaseEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (e.NewElement != null)
                {
                    // do whatever you want to the UITextField here!
                    var el = e.NewElement;
                    Control.BackgroundColor = el.BackgroundColor.ToUIColor();
                    Control.BorderStyle = UITextBorderStyle.None;
                }
            }
        }
    }
}