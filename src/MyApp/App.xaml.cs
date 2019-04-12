using System;
using System.Windows;
using Libs.Common;
using Libs.Res;
using MyApp.Libs;
using MyApp.Libs.Splash;

namespace MyApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            this.Startup += App_Startup;
            FilesHelper.MakeSureResourcesExist();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Prevent default unhandled exception processing
            var message = e.Exception.Message;
            MessageBox.Show(e.Exception.Message);
            e.Handled = true;
            try
            {
                DebugHelper.Resolve(null).Exception(message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            var ioc = IocHelper.Resolve();
            var configRepository = ioc.TryResolve<IMyAppConfigRepository>();
            var myAppConfig = configRepository.Get();
            var showSplash = GetShowSplash(myAppConfig);
            if (showSplash)
            {
                var shutdownMode = this.ShutdownMode;
                Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                var splashWindow = new SplashWindow();
                splashWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Splasher.AutoSplash(splashWindow, 1000, "页面加载中");
                this.ShutdownMode = shutdownMode;
            }

            var startupUri = GetStartupUri(myAppConfig);
            this.StartupUri = new Uri(startupUri, UriKind.Relative);
        }

        private string GetStartupUri(MyAppConfig myAppConfig)
        {
            if (myAppConfig != null)
            {
                if (!string.IsNullOrWhiteSpace(myAppConfig.StartupUri))
                {
                    return myAppConfig.StartupUri;
                }
            }
            return "MainWindow.xaml";
        }

        private bool GetShowSplash(MyAppConfig myAppConfig)
        {
            if (myAppConfig == null)
            {
                return false;
            }
            return myAppConfig.ShowSplash;
        }
    }
}
