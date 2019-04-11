using System.Collections.Generic;
using System.Linq;
using Libs.Common;

namespace MyApp.Libs
{
    public interface IMyAppConfigRepository
    {
        MyAppConfig Get();
        void Save(MyAppConfig config);
    }

    public class MyAppConfigRepository : IMyAppConfigRepository
    {
        public MyAppConfig Get()
        {
            return Helper.Read<MyAppConfig>(ConfigPath).FirstOrDefault();
        }

        public void Save(MyAppConfig config)
        {
            Helper.Save(ConfigPath, new List<MyAppConfig>(){config});
        }

        public MyAppConfigRepository(IFileDbHelper fileDbHelper, string configPath)
        {
            Helper = fileDbHelper;
            ConfigPath = configPath;
        }

        internal IFileDbHelper Helper { get; set; }
        internal string ConfigPath { get; set; }
    }
}
