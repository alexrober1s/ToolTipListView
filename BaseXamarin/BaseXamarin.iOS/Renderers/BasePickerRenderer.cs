using BaseXamarin.Custom;
using BaseXamarin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BasePicker), typeof(BasePickerRenderer))]
namespace BaseXamarin.iOS.Renderers
{
    public class BasePickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
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