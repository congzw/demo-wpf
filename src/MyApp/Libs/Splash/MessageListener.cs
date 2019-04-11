using System;
using System.Windows;

namespace MyApp.Libs.Splash
{
    public class MessageListener : DependencyObject
    {
        private static MessageListener _instance;

        private MessageListener()
        {

        }

        public static MessageListener Instance
        {
            get { return _instance ?? (_instance = new MessageListener()); }
        }

        public void ReceiveMessage(string message)
        {
            Message = message;
            Console.WriteLine(Message);
            DispatcherHelper.DoEvents();
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageListener), new UIPropertyMetadata(null));

    }
}
