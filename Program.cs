using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Threading;
using System.Runtime.InteropServices;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace Sleeper
{
    /*
     * ------------------------------------------------------------------------------------------------------------------
     * Sleeper by CapaciousCore licensed under MIT and...
     * ------------------------------------------------------------------------------------------------------------------
     * "THE BEER-WARE LICENSE" (Revision 42):
     * CapaciousCore wrote this software. As long as you retain this notice you can do whatever you want with this stuff.
     * If we meet some day, and you think this stuff is worth it, you can buy me a beer in return.
     * ------------------------------------------------------------------------------------------------------------------
     */

    static class Program
    {
        internal static Mutex Mutex;

        [STAThread]
        static void Main()
        {
            using (Mutex = new Mutex(false, @"Global\" + GetGuid()))
            {
                if (!Mutex.WaitOne(0, false))
                {
                    MessageBox.Show("An application instance is already running", "Sleeper", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TrayApplicationContext());
            }
        }

        private static Guid GetGuid()
        {
            GuidAttribute GuidAttribute = (GuidAttribute)Assembly.GetExecutingAssembly()?.GetCustomAttributes(typeof(GuidAttribute), false).SingleOrDefault();
            Guid.TryParse(GuidAttribute?.Value, out Guid GUID);

            return GUID;
        }
    }

    public class Configuration
    {
        public enum SelectionOperation { After, At };
        public enum OperationTypes { Shutdown, Restart, Log_off, Hibernate, Stand_by };
        public SelectionOperation SelectedOperation { set; get; }
        public OperationTypes AfterOperation { set; get; }
        public OperationTypes AtOperation { set; get; }
        public ushort TimeAfterOperation { set; get; }
        public ushort TimeAtOperation { set; get; }
        public bool IsWarnEnabled { set; get; }
        public bool IsEnabled { set; get; }
    }

    public class WarningConfiguration
    {
        public string Operation { set; get; }
        public ushort RemainingSeconds { set; get; }
    }

    internal struct LastInputInfo
    {
        public uint Size;
        public uint Time;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct TokenPrivileges
    {
        public int Count;
        public long Luid;
        public int Attr;
    }

    public class TrayApplicationContext : ApplicationContext
    {
        private NotifyIcon TrayIcon;
        private Configuration Config;
        private Timer IdleTimer = new Timer(interval: 1000);
        private Task Operation;
        private CancellationTokenSource OperationCancellationTokenSource;
        private CancellationTokenSource WarningCancellationTokenSource;

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LastInputInfo lii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        [DllImport("user32.dll")]
        private static extern int ExitWindowsEx(int uFlags, int dwReason);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, int DesiredAccess, ref IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string SystemName, string Name, ref long Luid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, bool DisableAllPrivileges, ref TokenPrivileges NewState, int BufferLength, IntPtr PreviousState, IntPtr ReturnLength);

        public TrayApplicationContext()
        {
            if (File.Exists("./Config.json"))
            {
                Config = JsonSerializer.Deserialize<Configuration>(File.ReadAllText("./Config.json"));
            }
            else
            {
                Config = new Configuration
                {
                    SelectedOperation = Sleeper.Configuration.SelectionOperation.After,
                    AfterOperation = Sleeper.Configuration.OperationTypes.Shutdown,
                    AtOperation = Sleeper.Configuration.OperationTypes.Shutdown,
                    TimeAfterOperation = 0,
                    TimeAtOperation = 0,
                    IsWarnEnabled = false,
                    IsEnabled = false
                };
            }

            IdleTimer.Elapsed += IdleTimerTick;

            if (Config.IsEnabled)
            {
                SwitchTimer();
            }

            ContextMenu ContextMenu = new ContextMenu();
            ContextMenu.MenuItems.Add("Configuration", Configuration);
            ContextMenu.MenuItems.Add("-");
            ContextMenu.MenuItems.Add("Exit", Exit);

            TrayIcon = new NotifyIcon
            {
                Visible = true,
                Text = "Sleeper",
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                ContextMenu = ContextMenu
            };

            Application.ApplicationExit += OnApplicationExit;
        }

        private void Configuration(object sender, EventArgs e)
        {
            if (GetFormInstance<ConfigurationForm>() == null)
            {
                ConfigurationForm ConfigurationForm = new ConfigurationForm(ref Config);
                ConfigurationForm.ChangeAction += SwitchTimer;
                ConfigurationForm.Show();
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            TrayIcon.Visible = false;
            Application.Exit();
        }

        private void SwitchTimer(bool Start = true)
        {
            if (Start)
            {
                if (Config.SelectedOperation == Sleeper.Configuration.SelectionOperation.After)
                {
                    WarningCancellationTokenSource = OperationCancellationTokenSource = null;
                    IdleTimer.Start();
                }
                else
                {
                    TimeSpan TimeAtOperation = TimeSpan.FromMinutes(Config.TimeAtOperation);
                    DateTime ScheduledTime = DateTime.Today + TimeAtOperation;

                    if (DateTime.Now > ScheduledTime)
                    {
                        ScheduledTime += TimeSpan.FromDays(1);
                    }

                    if (Config.IsWarnEnabled)
                    {
                        WarningCancellationTokenSource = new CancellationTokenSource();

                        if (ScheduledTime.AddMinutes(-1) > DateTime.Now)
                        {
                            Task.Delay(ScheduledTime.AddMinutes(-1) - DateTime.Now, WarningCancellationTokenSource.Token).ContinueWith(_ => ShowWarning());
                        }
                        else
                        {
                            Task.Run(() => ShowWarning((ushort)(ScheduledTime - DateTime.Now).TotalSeconds), WarningCancellationTokenSource.Token);
                        }
                    }

                    // Variant of using the TPL
                    OperationCancellationTokenSource = new CancellationTokenSource();
                    Operation = Task.Delay(ScheduledTime - DateTime.Now, OperationCancellationTokenSource.Token).ContinueWith(_ => ExecuteOperation());
                }
            }
            else
            {
                if (Config.SelectedOperation == Sleeper.Configuration.SelectionOperation.After)
                {
                    IdleTimer.Stop();
                }
                else
                {
                    if (Config.IsWarnEnabled)
                    {
                        WarningCancellationTokenSource.Cancel();
                    }

                    OperationCancellationTokenSource.Cancel();
                }

                HideWarning();
            }
        }

        private TForm GetFormInstance<TForm>() where TForm : Form
        {
            return Application.OpenForms.OfType<TForm>().FirstOrDefault();
        }

        private void IdleTimerTick(object sender, ElapsedEventArgs e)
        {
            var TimeAfterOperation = TimeSpan.FromMinutes(Config.TimeAfterOperation).TotalSeconds;
            long IdleTime = GetIdleTime();

            if (TimeAfterOperation == 0)
            {
                TimeAfterOperation = 60;
            }

            if (Config.IsWarnEnabled)
            {
                if (IdleTime >= TimeAfterOperation - 60)
                {
                    ShowWarning((ushort)(60 - IdleTime));
                }
                else
                {
                    HideWarning();
                }
            }

            if (IdleTime >= TimeAfterOperation)
            {
                ExecuteOperation();
            }
        }

        private static long GetIdleTime()
        {
            LastInputInfo LastInputInfo = new LastInputInfo();
            LastInputInfo.Size = (uint)Marshal.SizeOf(LastInputInfo);

            if (!GetLastInputInfo(ref LastInputInfo))
            {
                throw new Exception(GetLastError().ToString());
            }

            return (((Environment.TickCount & int.MaxValue) - (LastInputInfo.Time & uint.MaxValue)) & uint.MaxValue) / 1000;
        }

        private void ShowWarning(ushort RemainingSeconds = 60)
        {
            if (WarningCancellationTokenSource == null || !WarningCancellationTokenSource.IsCancellationRequested)
            {
                WarningForm WarningForm = GetFormInstance<WarningForm>();

                if (WarningForm == null)
                {
                    WarningConfiguration WarningConfiguration = new WarningConfiguration
                    {
                        Operation = Enum.GetName(typeof(Configuration.OperationTypes), (Config.SelectedOperation == Sleeper.Configuration.SelectionOperation.After ? Config.AfterOperation : Config.AtOperation)).ToLower().Replace('_', ' '),
                        RemainingSeconds = RemainingSeconds
                    };

                    WarningForm = new WarningForm(WarningConfiguration);
                    WarningForm.ShowDialog();
                }
                else
                {
                    WarningForm.UpdateRemainingSeconds(RemainingSeconds);
                }
            }
        }


        private void HideWarning()
        {
            WarningForm WarningForm = GetFormInstance<WarningForm>();

            if (WarningForm != null)
            {
                WarningForm.Invoke(new Action(() =>
                {
                    WarningForm.Close();
                }));
            }
        }

        private void ExecuteOperation()
        {
            if (OperationCancellationTokenSource == null || !OperationCancellationTokenSource.IsCancellationRequested)
            {
                Configuration.OperationTypes Operation = (Config.SelectedOperation == Sleeper.Configuration.SelectionOperation.After ? Config.AfterOperation : Config.AtOperation);

                switch (Operation)
                {
                    case Sleeper.Configuration.OperationTypes.Shutdown:
                        GetPrivilege();
                        ExitWindowsEx(1, 0);
                        break;

                    case Sleeper.Configuration.OperationTypes.Restart:
                        GetPrivilege();
                        ExitWindowsEx(2, 0);
                        break;

                    case Sleeper.Configuration.OperationTypes.Log_off:
                        ExitWindowsEx(0, 0);
                        // ExitWindowsEx(4, 0);
                        break;

                    case Sleeper.Configuration.OperationTypes.Hibernate:
                        Application.SetSuspendState(PowerState.Hibernate, true, true);
                        break;

                    case Sleeper.Configuration.OperationTypes.Stand_by:
                        Application.SetSuspendState(PowerState.Suspend, true, true);
                        break;
                }

                Application.Exit();
            }
        }

        private void GetPrivilege()
        {
            IntPtr TokenHandle = IntPtr.Zero;

            TokenPrivileges TokenPrivileges = new TokenPrivileges
            {
                Count = 1,
                Luid = 0,
                Attr = 2 // SE_PRIVILEGE_ENABLED
            };

            OpenProcessToken(Process.GetCurrentProcess().Handle, 40, ref TokenHandle); // Desired access is TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY
            LookupPrivilegeValue(null, "SeShutdownPrivilege", ref TokenPrivileges.Luid);
            AdjustTokenPrivileges(TokenHandle, false, ref TokenPrivileges, 0, IntPtr.Zero, IntPtr.Zero);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            Program.Mutex.ReleaseMutex();
            File.WriteAllText("./Config.json", JsonSerializer.Serialize(Config));
        }
    }
}