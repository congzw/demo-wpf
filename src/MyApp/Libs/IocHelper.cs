using System;
using Libs.Common;

namespace MyApp.Libs
{
    public interface IIocHelper
    {
        T TryResolve<T>();
    }

    public class IocHelper: IIocHelper
    {
        public T TryResolve<T>()
        {
            if (typeof(T) == typeof(IMyAppConfigRepository))
            {
                dynamic instance = new MyAppConfigRepository(
                    FileDbHelper.Resolve(), 
                    AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\MyAppConfig.json");
                return (T)instance;
            }

            if (typeof(T) == typeof(IFileDbHelper))
            {
                dynamic instance = FileDbHelper.Resolve();
                return (T)instance;
            }

            if (typeof(T) == typeof(IDebugHelper))
            {
                dynamic instance = DebugHelper.Resolve(null);
                return (T)instance;
            }

            if (typeof(T) == typeof(GifDemoHelper))
            {
                dynamic instance = new GifDemoHelper();
                return (T)instance;
            }

            return default(T);
        }

        #region for di extensions

        private static IIocHelper _resolve()
        {
            var helper = new IocHelper();
            return helper;
        }

        public static Func<IIocHelper> Resolve = _resolve;

        #endregion
    }
}
