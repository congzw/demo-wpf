using Libs.Common;
using MyApp.Properties;

namespace MyApp.Libs
{
    internal class VideoResourceHelper
    {
        public static void MakeSureFileExist(string path)
        {
            FileAutoCreate.Resolve().MakeSureFileExist(path, Resources.space, true);
        }
    }
}
