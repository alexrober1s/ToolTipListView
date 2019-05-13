using UIKit;

namespace BaseXamarin.iOS.Extensions
{
    public static class UIApplicationExtensions
    {
        // https://github.com/aritchie/support/blob/master/Acr.Support.iOS/Extensions.cs
        public static UIViewController GetTopViewController(this UIApplication app)
        {
            var viewController = app.KeyWindow.RootViewController;
            while (viewController.PresentedViewController != null)
                viewController = viewController.PresentedViewController;

            return viewController;
        }
    }
}
