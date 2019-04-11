using System;

namespace Libs.Common
{
    public class ProcessHelper: IProcessHelper
    {
        public string GetCurrentExePath()
        {
            var appPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            return appPath;
        }

        #region for di extensions

        private static IProcessHelper _resolve()
        {
            var debugHelper = new ProcessHelper();
            return debugHelper;
        }

        public static Func<IProcessHelper> Resolve = _resolve;

        #endregion
    }

    public interface IProcessHelper
    {
        string GetCurrentExePath();
    }
}
