using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Szoft2.Week10.BigBrother
{
    public delegate void AblakVáltásEventArgs(object sender, AlkalmazásHasználatEventArgs e);

    public partial class BigBrother : Form
    {

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, out Int32 lpdwProcessId);

        private string utolsóAbalkcím;
        private string utolsóAlakalmazásNév;
        private string utolsóLoginNév;
        private DateTime utolsóVáltozásIdőpontja;

        public BigBrother()
        {
            InitializeComponent();
            this.Show();
            this.TopMost = true;
            utolsóVáltozásIdőpontja = DateTime.Now;
        }

        public event AblakVáltásEventArgs AblakVáltás;

        private void timer1_Tick(object sender, EventArgs e)
        {
            string aktuálisAblakCím = GetAktívAblakCím();
            string aktuálisAlkalmazásNév = GetAktívAlkalmazásNév();
            string aktuálisLoginNév = GetAktívLoginNév();

            if (aktuálisAblakCím != utolsóAbalkcím || aktuálisAlkalmazásNév != utolsóAlakalmazásNév || aktuálisLoginNév != utolsóLoginNév)
            {
                AlkalmazásHasználatEventArgs ah = new AlkalmazásHasználatEventArgs();

                ah.Ablakcím = utolsóAbalkcím;
                ah.AlkalmazásNév = utolsóAlakalmazásNév;
                ah.LoginNév = utolsóLoginNév;

                TimeSpan elteltIdő = DateTime.Now - utolsóVáltozásIdőpontja;

                ah.Idő = elteltIdő.TotalSeconds;

                utolsóLoginNév = aktuálisLoginNév;
                utolsóAlakalmazásNév = aktuálisAlkalmazásNév;
                utolsóAbalkcím = aktuálisAblakCím;
                utolsóVáltozásIdőpontja = DateTime.Now;

                AblakVáltás(this, ah);

                labelTitle.Text = aktuálisAblakCím;
                labelApp.Text = aktuálisAlkalmazásNév;
                labelUser.Text = aktuálisLoginNév;
            }
        }

        private string GetAktívLoginNév()
        {
            return WindowsIdentity.GetCurrent().Name;
        }

        private string GetAktívAlkalmazásNév()
        {
            IntPtr handle = IntPtr.Zero;
            handle = GetForegroundWindow();

            string appProcessName = "?";

            try
            {
                appProcessName = Process.GetProcessById(GetWindowProcessID(handle)).ProcessName;
            }
            catch
            {
            }

            return appProcessName;
        }

        private string GetAktívAblakCím()
        {
            const int nChars = 256;
            IntPtr handle = IntPtr.Zero;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        private Int32 GetWindowProcessID(IntPtr hwnd)
        {
            Int32 pid = 1;
            GetWindowThreadProcessId(hwnd, out pid);
            return pid;
        }
    }

    public class AlkalmazásHasználatEventArgs
    {
        public string AlkalmazásNév;
        public string Ablakcím;
        public string LoginNév;
        public double Idő;
    }
}
