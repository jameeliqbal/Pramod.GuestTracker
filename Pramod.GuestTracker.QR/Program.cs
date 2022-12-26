using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pramod.GuestTracker.QR
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("ODE3NTQzQDMyMzAyZTM0MmUzMFA4TVN1THBCSmdoNEF3WHh3MXAxMjZsODdsTHY1SW0xd2tZR3J4VFJEVmM9");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
