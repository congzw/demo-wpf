using System;
using System.Windows;
using Libs.Common;
using MyApp.Libs;

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
            if (myAppConfig != null)
            {
                if (!string.IsNullOrWhiteSpace(myAppConfig.StartupUri))
                {
                    this.StartupUri = new Uri(myAppConfig.StartupUri, UriKind.Relative);
                    return;
                }
            }

            this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
        }
    }
}
