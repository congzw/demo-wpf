using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Libs.Common;
using MyApp.Libs;
using MyApp.ViewModel;

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

        public PolicyDemoViewModel PolicyDemo { get; set; }
        public CommandDemoViewModel CommandDemo { get; set; }

        private void SetupIoc()
        {
            var ioc = IocHelper.Resolve();
            ConfigRepository = ioc.TryResolve<IMyAppConfigRepository>();
            DebugHelper = ioc.TryResolve<IDebugHelper>();
            GifDemoHelper = ioc.TryResolve<GifDemoHelper>();
        }
        
        public IMyAppConfigRepository ConfigRepository { get; set; }
        public IDebugHelper DebugHelper { get; set; }
        public GifDemoHelper GifDemoHelper { get; set; }

        private void CustomizeInitializeComponent()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            PolicyDemo = new PolicyDemoViewModel();
            PolicyDemo.Init(this);

            CommandDemo = new CommandDemoViewModel();
            CommandDemo.Init(this);

            GifDemoHelper.Append(MyGif);
            this.GridFront.Visibility = Visibility.Collapsed;
            this.Loaded += DemoWindow_Loaded;
            this.BtnDebug.Click += BtnDebug_Click;
            this.BtnException.Click += BtnException_Click;
            this.BtnLoadConfig.Click += BtnLoadConfig_Click;
            this.BtnPlay.Click += BtnPlay_Click;
            this.BtnGif.Click += BtnGif_Click;
        }

        private void DemoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CreateAndPlay(GridBackground, true);
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

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (_playing)
            {
                ShowMessage("playing, wait!");
                return;
            }
            CreateAndPlay(GridBackground, false);
        }

        private void BtnGif_Click(object sender, RoutedEventArgs e)
        {
            GifDemoHelper.ShowGif();
        }

        public void ShowMessage(string message)
        {
            this.TxtMessage.Text = message;
            this.TxtMessage.Visibility = Visibility.Visible;
        }

        //解决多次播放的问题
        private bool _playing = false;
        private void CreateAndPlay(Panel panel, bool checkConfig)
        {
            if (checkConfig)
            {
                var config = ConfigRepository.Get();
                if (config != null)
                {
                    if (!config.ShowVideo)
                    {
                        ShowMessage("MediaDisabled");
                        this.GridFront.Visibility = Visibility.Visible;
                        return;
                    }
                }
            }
            
            _playing = true;
            var mediaElement = new MediaElement();
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Close;
            var videoFile = "video/sample.mp4";
            ResourceFileHelper.MakeSureVideoExist(videoFile);
            mediaElement.Source = new Uri(videoFile, UriKind.Relative);
            mediaElement.Stretch = Stretch.Fill;

            mediaElement.MediaOpened += (sender, args) =>
            {
                ShowMessage("MediaOpened");
            };

            mediaElement.MediaEnded += (sender, args) =>
            {
                ShowMessage("MediaEnded");
                ClearVideo(ref panel, ref mediaElement);
            };

            mediaElement.MediaFailed += (sender, args) =>
            {
                ShowMessage("MediaFailed" + args.ErrorException.Message);
                ClearVideo(ref panel, ref mediaElement);
            };

            panel.Children.Add(mediaElement);
            mediaElement.Play();
        }

        private void ClearVideo(ref Panel panel, ref MediaElement mediaElement)
        {
            panel.Children.Remove(mediaElement);
            mediaElement = null;
            _playing = false;
            this.GridFront.Visibility = Visibility.Visible;
        }
    }
}
