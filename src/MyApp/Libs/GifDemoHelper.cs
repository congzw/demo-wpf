using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Libs.Res;
using XamlAnimatedGif;

namespace MyApp.Libs
{
    public class GifDemoHelper
    {
        public GifDemoHelper()
        {
            Gif = CreateGif();
        }

        public Image Gif { get; set; }

        private Image CreateGif()
        {
            //<Image x:Name="ImgLoading" VerticalAlignment="Center" HorizontalAlignment="Center" Stretch="None" />
            var gifImage = new Image();
            gifImage.VerticalAlignment = VerticalAlignment.Center;
            gifImage.HorizontalAlignment = HorizontalAlignment.Center;
            gifImage.Stretch = Stretch.None;
            return gifImage;
        }

        public void Append(Panel panel)
        {
            if (panel.Children.Contains(Gif))
            {
                return;
            }
            panel.Children.Add(Gif);
        }

        private readonly string[] _gifs = new[] {FilesHelper.Loading01Gif, FilesHelper.Loading02Gif};

        private int index = 0;
        public void ShowGif()
        {
            index++;
            var picIndex = index % 2;
            var gif = _gifs[picIndex];
            var picPath = AppDomain.CurrentDomain.BaseDirectory + gif;
            //Console.WriteLine(picPath);
            AnimationBehavior.SetSourceUri(Gif, new Uri(picPath, UriKind.Absolute));
        }
    }
}
