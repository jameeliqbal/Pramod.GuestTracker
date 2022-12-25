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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Njg4ODdAMzEzNjJlMzQyZTMwSDVkaEpYS3ZETWlXUkVMMGZ3TzBOL295WUpHSnJuNWcwZWlleWN1dnhXYz0=");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
