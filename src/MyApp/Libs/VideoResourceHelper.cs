using System;
using System.IO;
using MyApp.Properties;

namespace MyApp.Libs
{
    internal class VideoResourceHelper
    {
        public static void MakeSureFileExist(string path)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            if (File.Exists(file))
            {
                return;
            }

            var directoryName = Path.GetDirectoryName(file);
            if (!string.IsNullOrWhiteSpace(directoryName) && !Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            File.WriteAllBytes(file, Resources.space);
        }
    }
}
