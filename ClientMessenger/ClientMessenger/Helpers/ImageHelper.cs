using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClientMessenger.Helpers
{
    public static class ImageHelper
    {
        public static BitmapImage ByteToImageSource(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            using (MemoryStream byteStream = new MemoryStream(data))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = byteStream;
                image.EndInit();
                return image;
            }
        }

        public static byte[] ImageSourceToByte(ImageSource image)
        {
            var bitmap = image as BitmapImage;
            if (bitmap == null)
            {
                return null;
            }

            return ImageSourceToByte(bitmap);
        }

        public static byte[] ImageSourceToByte(BitmapImage image)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            byte[] bitmapBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                bitmapBytes = ms.ToArray();
            }

            return bitmapBytes;
        }


        public static BitmapImage GetImage(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            var uri = new Uri(filePath, UriKind.Relative);
            return new BitmapImage(uri);
        }
    }
}
