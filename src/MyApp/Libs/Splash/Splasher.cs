using System;
using System.Threading;
using System.Windows;

namespace MyApp.Libs.Splash
{
    public static class Splasher
    {
        private static Window _splashWindow;

        public static Window SplashWindow
        {
            get
            {
                return _splashWindow;
            }
            set
            {
                _splashWindow = value;
            }
        }

        public static void ShowSplash()
        {
            if (_splashWindow != null)
            {
                _splashWindow.Show();
            }
        }

        public static void CloseSplash()
        {
            if (_splashWindow != null)
            {
                _splashWindow.Close();
                if (_splashWindow is IDisposable)
                {
                    (_splashWindow as IDisposable).Dispose();
                }
            }
        }

        public static void AutoSplash(Window splashWindow, int milliseconds, string message)
        {
            SplashWindow = splashWindow;
            ShowSplash();

            var sleepCount = milliseconds / 100;
            for (int i = 0; i < 100; i++)
            {
                MessageListener.Instance.ReceiveMessage(string.Format("{0} {1}%", message, i));
                Thread.Sleep(sleepCount);
            }
            CloseSplash();
        }
    }
}
