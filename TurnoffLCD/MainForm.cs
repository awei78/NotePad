using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CloseLCD
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            UpdateUIState();
        }

        #region 加入about菜单
        // P/Invoke constants
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;

        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        // ID for the About item on the system menu
        private int SYSMENU_ABOUT_ID = 0x1;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);
            // Add a separator
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);
            // Add the About menu item
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "关于(&A)...");
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_ABOUT_ID))
            {
                MessageBox.Show(this, "此程序可在桌面及文件夹右键菜单中加入[关闭显示器]菜单项。\r\n\r\n作者：楚人无衣（刘景威）\r\n版本：" + Application.ProductVersion, "关于...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        private void UpdateUIState()
        {
            btnSetMenuItem.Text = MenuItemExists() ? "从右键菜单移除" : "加入桌面右键菜单";
            txtMenuText.Enabled = !MenuItemExists();
        }

        private bool RunAsAdministrator()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Application.ExecutablePath,
                    Verb = "runas"
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool MenuItemExists()
        {
            var path = Registry.GetValue(@"HKEY_CLASSES_ROOT\Directory\Background\shell\TurnoffMonitor\command", "", "");
            if (path == null)
                return false;
            return File.Exists(path.ToString().Replace("\"", "").Replace(" -silent", ""));
        }

        private void InstallMenuItem()
        {
            try
            {
                var key = Registry.ClassesRoot.CreateSubKey(@"Directory\Background\shell\TurnoffMonitor");
                if (key != null)
                {
                    string text = !string.IsNullOrEmpty(txtMenuText.Text) ? txtMenuText.Text : "关闭显示器(&M)";
                    key.SetValue("", text, RegistryValueKind.String);
                    string exePath = string.Format("\"{0}\"", Application.ExecutablePath);
                    key.SetValue("Icon", exePath, RegistryValueKind.String);
                    key.SetValue("Position", "Bottom", RegistryValueKind.String);
                    key = key.CreateSubKey("command");
                    key.SetValue("", exePath + " -silent", RegistryValueKind.String);
                    key.Close();
                }
            }
            catch (UnauthorizedAccessException)
            {
                if (MessageBox.Show(this, "加入菜单项失败，请确认是否有杀毒软件等工具防护注册表。\r\n或者，您需要以管理员身份运行此程序。\r\n现在以管理员身份重启程序，然后再设置，如何？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && this.RunAsAdministrator())
                    Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "加入菜单项失败，请确认是否有杀毒软件等工具防护注册表。\r\n或者，您需要以管理员身份运行此程序。\r\n信息为：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private const int SC_MONITORPOWER = 0xF170;                  //关闭显示器的系统命令
        private const int MonitorPowerOff = 2;                       //2为PowerOff, 1为省电状态，-1为开机
        private static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);  //广播消息，所有顶级窗体都会接收

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        public static void TurnOffMonitor()
        {
            Thread.Sleep(500);
            SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, MonitorPowerOff);
        }

        private void btnTurnoff_Click(object sender, EventArgs e)
        {
            TurnOffMonitor();
        }

        private void btnSetMenuItem_Click(object sender, System.EventArgs e)
        {
            //加入
            if (!MenuItemExists())
                InstallMenuItem();
            //移除
            else
            {
                try
                {
                    using (var key = Registry.ClassesRoot.OpenSubKey(@"Directory\Background\shell\TurnoffMonitor", true))
                    {
                        if (key != null)
                            key.DeleteSubKeyTree("");
                    }
                }
                catch (SecurityException)
                {
                    if (MessageBox.Show(this, "移除菜单项失败，请确认是否有杀毒软件等工具防护注册表。\r\n或者，您需要以管理员身份运行此程序。\r\n现在以管理员身份重启程序，然后再去除，如何？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && this.RunAsAdministrator())
                        Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "移除菜单项失败，请确认是否有杀毒软件等工具防护注册表。\r\n或者，您需要以管理员身份运行此程序。\r\n信息为：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            UpdateUIState();
        }
    }
}
