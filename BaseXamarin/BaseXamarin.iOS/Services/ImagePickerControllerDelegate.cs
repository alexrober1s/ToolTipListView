using System;
using Foundation;
using UIKit;

namespace BaseXamarin.iOS.Services.Camera
{
    public class ImagePickerControllerDelegate : UIImagePickerControllerDelegate
    {
        public Action<UIImage> ImagePicked { get; private set; }
        public Action Cancelled { get; private set; }

        public ImagePickerControllerDelegate(Action<UIImage> imagePicked, Action cancelled)
        {
            ImagePicked = imagePicked;
            Cancelled = cancelled;
        }

        public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            var image = info.ValueForKey(UIImagePickerController.OriginalImage) as UIImage;

            picker.DismissViewController(true, () =>
            {
                ImagePicked?.Invoke(image);
            });
        }

        public override void Canceled(UIImagePickerController picker)
        {
            picker.DismissViewController(true, () =>
            {
                Cancelled?.Invoke();
            });
        }
    }
}
