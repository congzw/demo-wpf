using Libs.Common;
using Microsoft.Win32;

namespace MyApp.ViewModel
{
    public class PolicyDemoViewModel
    {
        public DemoWindow DemoWindow { get; set; }
        public IGroupPolicyHelper Policy { get; set; }
        public IProcessHelper Process { get; set; }

        public const string ShellKey = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\System!Shell";

        public void Init(DemoWindow demoWindow)
        {
            DemoWindow = demoWindow;
            Policy = GroupPolicyHelper.Resolve();
            Process = ProcessHelper.Resolve();
            demoWindow.BtnSetShell.Click += BtnSetShell_Click;
            demoWindow.BtnGetShell.Click += BtnGetShell_Click;
            demoWindow.BtnResetShell.Click += BtnResetShell_Click;
        }

        private void BtnResetShell_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Policy.SetValue(ShellKey, null, RegistryValueKind.String);
            var value = Policy.GetValue(ShellKey, "NULL");
            DemoWindow.ShowMessage("Reset Shell: " + value);
        }

        private void BtnGetShell_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var value = Policy.GetValue(ShellKey, "NULL");
            DemoWindow.ShowMessage("Get Shell: " + value);
        }

        private void BtnSetShell_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var appPath = Process.GetCurrentExePath();
            Policy.SetValue(ShellKey, appPath, RegistryValueKind.String);
            DemoWindow.ShowMessage("Set Shell: " + appPath);

            var value = Policy.GetValue(ShellKey, "NULL");
            DemoWindow.ShowMessage("Get Shell: " + value);
        }
    }
}
