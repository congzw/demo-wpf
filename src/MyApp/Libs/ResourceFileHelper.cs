using System.Drawing;
using System.IO;
using Libs.Common;
using MyApp.Properties;

namespace MyApp.Libs
{
    internal class ResourceFileHelper
    {
        private static readonly IFileAutoCreate AutoFile = FileAutoCreate.Resolve();

        public static void MakeSureVideoExist(string path)
        {
            AutoFile.MakeSureFileExist(path, Resources.space, true);
        }

        public static void MakeSureGifExist(string path, Image image)
        {
            AutoFile.MakeSureFileExist(path, ImageToByteArray(image), true);
        }

        private static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }
        
        private static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
