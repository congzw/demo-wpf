using System;
using System.Windows;
using Libs.Common;

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
            //todo load by config
            this.StartupUri = new Uri("DemoWindow.xaml", UriKind.Relative);
        }
    }
}
