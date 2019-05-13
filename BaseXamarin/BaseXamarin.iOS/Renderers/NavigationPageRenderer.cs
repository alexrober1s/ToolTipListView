using BaseXamarin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NoLineNavigationRenderer))] 
namespace BaseXamarin.iOS.Renderers
{
    public class NoLineNavigationRenderer : NavigationRenderer
    {

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            NavigationBar.ShadowImage = new UIImage();
        }
    }
}