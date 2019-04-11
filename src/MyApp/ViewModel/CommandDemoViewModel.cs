﻿using Libs.Common;

namespace MyApp.ViewModel
{
    public class CommandDemoViewModel
    {
        public DemoWindow DemoWindow { get; set; }
        public ICommandHelper Command { get; set; }

        public void Init(DemoWindow demoWindow)
        {
            DemoWindow = demoWindow;
            Command = CommandHelper.Resolve();

            demoWindow.BtnLogoff.Click += BtnLogoff_Click;
            demoWindow.BtnShutdown.Click += BtnShutdown_Click;
            demoWindow.BtnRestart.Click += BtnRestart_Click;
            demoWindow.BtnExplorer.Click += BtnExplorer_Click;
        }

        private void BtnExplorer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Command.Run("explorer.exe");
        }

        private void BtnShutdown_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Command.Run("shutdown -s -t 3");
        }

        private void BtnLogoff_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Command.Run("shutdown -L");
        }

        private void BtnRestart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Command.Run("shutdown -r -t 3");
        }
    }
}
