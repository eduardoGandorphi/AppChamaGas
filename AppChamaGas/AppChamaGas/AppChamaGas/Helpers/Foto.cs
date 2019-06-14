using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppChamaGas.Helpers
{
    public class Foto
    {
        public static async Task<ImageSource> ChamaCamera()
        {
            //await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsPickPhotoSupported && CrossMedia.Current.IsTakePhotoSupported)
            {
                //var file = await CrossMedia
                //    .Current
                //    .TakePhotoAsync(new StoreCameraMediaOptions
                //    {
                //        Directory = "Images",
                //        Name = $"{DateTime.Now}_test.jpg",
                //    });

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Test",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Front
                });

                //var file = await CrossMedia.Current.PickPhotoAsync();
                return ImageSource.FromStream(file.GetStream);
            }
            else
                return null;

        }
    }
}
