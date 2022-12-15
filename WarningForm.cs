using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;
using Timer = System.Timers.Timer;

namespace Sleeper
{
    public partial class WarningForm : Form
    {
        private WarningConfiguration WarningConfiguration;
        private Timer CountdownTimer = new Timer(interval: 1000);

        // [DllImport("user32.dll")]
        // [return: MarshalAs(UnmanagedType.Bool)]
        // private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public WarningForm(WarningConfiguration Config)
        {
            WarningConfiguration = Config;
            InitializeComponent();
        }

        internal void UpdateRemainingSeconds(ushort RemainingSeconds)
        {
            WarningConfiguration.RemainingSeconds = RemainingSeconds;
        }

        private void LoadWarningForm(object sender, EventArgs e)
        {
            CountdownTimer.Elapsed += CountdownTimerTick;
            CountdownTimer.Start();
            // Centering window position
            Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
            // Set HWND_TOPMOST if you like
            // SetWindowPos(Handle, new IntPtr(-1), (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2, Bounds.Width, Bounds.Height, 0);
        }

        private void CountdownTimerTick(object sender, ElapsedEventArgs e)
        {
            if (WarningConfiguration.RemainingSeconds > 0)
            {
                WarningConfiguration.RemainingSeconds -= 1;
                Invalidate();
            }
            else
            {
                CountdownTimer.Stop();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Font Font = new Font("Microsoft Sans Serif", 22))
            {
                StringFormat StringFormat = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                string Statement = "Computer will " + WarningConfiguration.Operation + (WarningConfiguration.RemainingSeconds > 0 ? " in " + WarningConfiguration.RemainingSeconds + " second" + (WarningConfiguration.RemainingSeconds > 1 ? "s" : "") : " now");
                e.Graphics.DrawString(Statement, Font, Brushes.Black, ClientRectangle, StringFormat);
            }
        }

        // prevent the window from closing with ALT + F4 action
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (new StackTrace(8, false).GetFrame(0).GetMethod().Name == "DefWndProc")
            {
                e.Cancel = true;
            }

            base.OnFormClosing(e);
        }

        // Prevent window from showing in alt tab list
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams CreateParams = base.CreateParams;
                CreateParams.ExStyle |= 0x80; // WS_EX_TOOLWINDOW

                return CreateParams;
            }
        }
    }
}