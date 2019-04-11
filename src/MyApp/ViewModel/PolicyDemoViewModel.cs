using System.Text;
using Libs.Common;
using Microsoft.Win32;

namespace MyApp.ViewModel
{
    public class PolicyDemoViewModel
    {
        public DemoWindow DemoWindow { get; set; }
        public IGroupPolicyHelper Policy { get; set; }
        public IProcessHelper Process { get; set; }
        public ShellHelper Shell { get; set; }

        public const string ShellKey = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\System!Shell";

        public void Init(DemoWindow demoWindow)
        {
            DemoWindow = demoWindow;
            Policy = GroupPolicyHelper.Resolve();
            Process = ProcessHelper.Resolve();
            Shell = new ShellHelper();
            demoWindow.BtnSetShell.Click += BtnSetShell_Click;
            demoWindow.BtnGetShell.Click += BtnGetShell_Click;
            demoWindow.BtnResetShell.Click += BtnResetShell_Click;
        }

        private void BtnResetShell_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Shell.Reset();
            var value = Shell.GetValue("NULL");
            DemoWindow.ShowMessage("Reset Shell: " + value);

            //Policy.SetValue(ShellKey, null, RegistryValueKind.String);
            //var value = Policy.GetValue(ShellKey, "NULL");
            //DemoWindow.ShowMessage("Reset Shell: " + value);
        }

        private void BtnGetShell_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var value = Shell.GetValue("NULL");
            DemoWindow.ShowMessage("Get Shell: " + value);

            //var value = Policy.GetValue(ShellKey, "NULL");
            //DemoWindow.ShowMessage("Get Shell: " + value);
        }

        private void BtnSetShell_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var appPath = Process.GetCurrentExePath();
            Shell.SetValue(appPath);
            DemoWindow.ShowMessage("Set Shell: " + appPath);

            var value = Shell.GetValue("NULL");
            DemoWindow.ShowMessage("Get Shell: " + value);

            //var appPath = Process.GetCurrentExePath();
            //Policy.SetValue(ShellKey, appPath, RegistryValueKind.String);
            //DemoWindow.ShowMessage("Set Shell: " + appPath);

            //var value = Policy.GetValue(ShellKey, "NULL");
            //DemoWindow.ShowMessage("Get Shell: " + value);
        }
    }

    public class ShellHelper
    {
        public ShellHelper()
        {
            //ShellKeyForCurrentUser = @"HKEY_Current_User\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
            ShellKeyForCurrentUser = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
        }

        public string ShellKeyForCurrentUser { get; set; }

        public void SetValue(string shellValue)
        {
            Registry.SetValue(ShellKeyForCurrentUser, "Shell", shellValue);
        }

        public void Reset()
        {
            Registry.SetValue(ShellKeyForCurrentUser, "Shell", null);
        }
        public object GetValue(object defaultValue)
        {
            return Registry.GetValue(ShellKeyForCurrentUser, "Shell", defaultValue);
        }
    }
}
