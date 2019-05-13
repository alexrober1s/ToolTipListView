using CoreGraphics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UIKit;
using BaseXamarin.Services;
using BaseXamarin.iOS.Extensions;
using BaseXamarin.iOS.Services.Camera;

namespace BaseXamarin.iOS.Services
{
    public class CameraService : ICameraService
    {
        private const string TypeImage = "public.image";

        public CameraService()
        {
            bool isCameraAvailable = UIImagePickerController.IsCameraDeviceAvailable(UIKit.UIImagePickerControllerCameraDevice.Front)
                                       | UIImagePickerController.IsCameraDeviceAvailable(UIKit.UIImagePickerControllerCameraDevice.Rear);

            var availableCameraMedia = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.Camera) ?? new string[0];
            var avaialbleLibraryMedia = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary) ?? new string[0];

            IsTakePhotoSupported = false;
            IsPickPhotoSupported = false;
            foreach (string type in availableCameraMedia.Concat(avaialbleLibraryMedia))
            {
                if (type == TypeImage)
                {
                    IsTakePhotoSupported = true;
                    IsPickPhotoSupported = true;
                }
            }
        }

        public bool IsTakePhotoSupported { get; private set; }
        public bool IsPickPhotoSupported { get; private set; }

        public Task<Stream> ChoosePictureAsync(int maxPixelDimension, int quality)
        {
            return TakeOrChoosePictureAsync(maxPixelDimension, quality, UIImagePickerControllerSourceType.PhotoLibrary);
        }

        public Task<Stream> TakePictureAsync(int maxPixelDimension, int quality)
        {
            return TakeOrChoosePictureAsync(maxPixelDimension, quality, UIImagePickerControllerSourceType.Camera);
        }

        private Task<Stream> TakeOrChoosePictureAsync(int maxPixelDimension, int quality, UIImagePickerControllerSourceType mode, UIImagePickerControllerCameraDevice? device = null)
        {
            var tcs = new TaskCompletionSource<Stream>();
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var imagePickerController = new UIImagePickerController()
                {
                    SourceType = mode
                };

                if (device.HasValue)
                {
                    imagePickerController.CameraDevice = device.Value;
                }


                var del = new ImagePickerControllerDelegate(image => {
                    tcs.TrySetResult(ImagePicked(image, maxPixelDimension, quality));
                }, ()=>{
                    tcs.TrySetResult(null);
                });

                imagePickerController.Delegate = del;

                var topVC = UIApplication.SharedApplication.GetTopViewController() as UIViewController;
                topVC.PresentViewController(imagePickerController, true, null);
            });

            return tcs.Task;
        }

        private Stream ImagePicked(UIImage image, int maxDimension, int quality)
        {
            if (image.Size.Height > maxDimension | image.Size.Width > maxDimension)
            {
                image = image.ImageToFitSize(new CGSize(maxDimension, maxDimension));
            }

            return (quality < 100 ? image.AsJPEG(quality / 100f) : image.AsJPEG()).AsStream();
        }
    }
}
