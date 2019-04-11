using System.Windows;
using System.Windows.Media;

namespace MyApp
{
    /// <summary>
    /// SplashWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
            CustomizeInitializeComponent();
        }

        private void CustomizeInitializeComponent()
        {
            this.Title = "Loading";
            this.Height = 400;
            this.Width = 600;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowStyle = WindowStyle.None;
            this.Background = Brushes.Orange;
            this.BorderBrush = Brushes.DarkOrange;
            this.BorderThickness = new Thickness(3);
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
        }
    }
}
