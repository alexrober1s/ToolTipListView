using System;
using System.Collections.Generic;
using System.Linq;
using BaseXamarin.iOS.Services;
using BaseXamarin.Services;
using Foundation;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin.File;
using MvvmCross.Plugin.File.Platforms.Ios;
using UIKit;
using Xamarin.Forms;

namespace BaseXamarin.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Forms.Init();
            InitStyle();

            // Init Mvx Ioc
            new Setup().InitializePrimary();

            // Init DI
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ICameraService, CameraService>();

            // File Store Service Setup
            var fileStoreConfig = new MvxFileConfiguration(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            var fileStore = new MvxIosFileStore(fileStoreConfig.AppendDefaultPath, fileStoreConfig.BasePath);
            Mvx.IoCProvider.RegisterSingleton<IMvxFileStore>(fileStore);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        void InitStyle()
        {
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(42, 45, 67);
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White };
        }
    }
}



