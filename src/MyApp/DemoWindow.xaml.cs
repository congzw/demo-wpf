using System;
using System.Windows;
using Libs.Common;

namespace MyApp
{
    /// <summary>
    /// DemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DemoWindow : Window
    {
        public DemoWindow()
        {
            InitializeComponent();
            CustomizeInitializeComponent();
        }

        private void CustomizeInitializeComponent()
        {
            this.BtnDebug.Click += BtnDebug_Click;
            this.BtnException.Click += BtnException_Click;
        }

        private void BtnDebug_Click(object sender, RoutedEventArgs e)
        {
            DebugHelper.Resolve(null).Debug("whatever!");
        }

        private void BtnException_Click(object sender, RoutedEventArgs e)
        {
            throw new InvalidOperationException("whatever!");
        }
    }
}
