using System;
using System.Windows;
using Libs.Common;
using MyApp.Libs;

namespace MyApp
{
    /// <summary>
    /// DemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DemoWindow : Window
    {
        public DemoWindow()
        {
            SetupIoc();
            InitializeComponent();
            CustomizeInitializeComponent();
        }

        private void SetupIoc()
        {
            var ioc = IocHelper.Resolve();
            ConfigRepository = ioc.TryResolve<IMyAppConfigRepository>();
            DebugHelper = ioc.TryResolve<IDebugHelper>();
        }

        public IMyAppConfigRepository ConfigRepository { get; set; }
        public IDebugHelper DebugHelper { get; set; }

        private void CustomizeInitializeComponent()
        {
            this.BtnDebug.Click += BtnDebug_Click;
            this.BtnException.Click += BtnException_Click;
            this.BtnLoadConfig.Click += BtnLoadConfig_Click;
        }

        private void BtnDebug_Click(object sender, RoutedEventArgs e)
        {
            DebugHelper.Debug("whatever!");
        }

        private void BtnException_Click(object sender, RoutedEventArgs e)
        {
            throw new InvalidOperationException("whatever!");
        }

        private void BtnLoadConfig_Click(object sender, RoutedEventArgs e)
        {
            var config = ConfigRepository.Get();
            if (config != null)
            {
                ShowMessage(config.ToJson(false));
                return;
            }

            config = new MyAppConfig();
            MessageBox.Show("初次创建");
            ConfigRepository.Save(config);
        }

        private void ShowMessage(string message)
        {
            this.TxtMessage.Text = message;
            this.TxtMessage.Visibility = Visibility.Visible;
        }
    }
}
