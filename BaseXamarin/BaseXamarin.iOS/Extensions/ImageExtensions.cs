using System;
using CoreGraphics;
using UIKit;

namespace BaseXamarin.iOS.Extensions
{
    public static class ImageExtensions
    {
        public static UIImage ImageToFitSize(this UIImage image, CGSize fitSize)
        {
            var imageScaleFactor = (double)image.CurrentScale;

            var sourceWidth = image.Size.Width * imageScaleFactor;
            var sourceHeight = image.Size.Height * imageScaleFactor;
            var targetWidth = fitSize.Width;
            var targetHeight = fitSize.Height;

            var sourceRatio = sourceWidth / sourceHeight;
            var targetRatio = targetWidth / targetHeight;

            var scaleWidth = sourceRatio <= targetRatio;
            scaleWidth = !scaleWidth;

            double scalingFactor;
            double scaledWidth;
            double scaledHeight;

            if (scaleWidth)
            {
                scalingFactor = 1.0 / sourceRatio;
                scaledWidth = targetWidth;
                scaledHeight = Math.Round(targetWidth * scalingFactor);
            }
            else
            {
                scalingFactor = sourceRatio;
                scaledWidth = Math.Round(targetHeight * scalingFactor);
                scaledHeight = targetHeight;
            }

            var destRect = new CGRect(0, 0, (nfloat)scaledWidth, (nfloat)scaledHeight);

            UIGraphics.BeginImageContextWithOptions(destRect.Size, false, 1);
            image.Draw(destRect);

            var newImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return newImage;
        }
    }
}
