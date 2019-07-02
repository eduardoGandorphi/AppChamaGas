using AppChamaGas.Extensions;
using AppChamaGas.Models;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppChamaGas.Helpers
{
    public class Photo
    {
        public static async Task<Foto_MD> TiraFoto(string nomeFoto = "test.jpg", string dir = "myDir", 
            bool saveInAlbum = true )
        {
            var md = new Foto_MD();

            var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                Name = nomeFoto,
                Directory = dir,
                SaveToAlbum = saveInAlbum,
                CompressionQuality = 10,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                CustomPhotoSize =10,
            });

            md.PathGaleria = photo.AlbumPath;
            md.PathInterno = photo.Path;
            md.fotoArray = photo.GetStream().ToByteArray();

            return md;
        }
    }
}
