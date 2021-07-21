using System;
using System.IO;
using System.Windows.Forms;
using zupit.zupitPDF.SetupHelper.Helper;

namespace zupit.zupitPDF.SetupHelper.Driver
{
    internal class Actions
    {
        public static bool CheckIfPrinterNotInstalled()
        {
            bool resultCode;
            zupitPDFInstaller installer = new zupitPDFInstaller();
            try
            {
                if (installer.IszupitPDFPrinterInstalled())
                    resultCode = true;
                else
                    resultCode = false;
            }
            finally
            { }
            return resultCode;
        }

        public static bool AddPrinter(string name)
        {
            bool resultCode;
            zupitPDFInstaller installer = new zupitPDFInstaller();
            try
            {
                if (installer.AddCustomzupitPDFPrinter(name))
                {
                    resultCode = true;
                    Spooler.stop();
                    Spooler.start();
                }
                else
                    resultCode = false;
            }
            finally
            { }
            return resultCode;
        }

        public static bool RemovePrinter(string name)
        {
            bool resultCode;
            zupitPDFInstaller installer = new zupitPDFInstaller();
            try
            {
                if (installer.DeleteCustomzupitPDFPrinter(name))
                    resultCode = true;
                else
                    resultCode = false;
            }
            finally
            { }
            return resultCode;
        }

        public static bool InstallzupitPDFPrinter()
        {
            bool printerInstalled;
            string clawmonpath;
            zupitPDFInstaller installer = new zupitPDFInstaller();
            try
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    clawmonpath = Path.GetDirectoryName(Application.ExecutablePath) + @"\clawmon\x64\";
                }
                else
                {
                    clawmonpath = Path.GetDirectoryName(Application.ExecutablePath) + @"\clawmon\x86\";
                }

                if (installer.InstallzupitPDFPrinter(clawmonpath, "zupitPDF.exe"))
                    printerInstalled = true;
                else
                    printerInstalled = false;
            }
            finally
            { }
            return printerInstalled;
        }

        public static bool UninstallzupitPDFPrinter()
        {
            bool printerUninstalled;
            zupitPDFInstaller installer = new zupitPDFInstaller();
            try
            {
                if (installer.UninstallzupitPDFPrinter())
                    printerUninstalled = true;
                else
                    printerUninstalled = false;
            }
            finally
            { }
            return printerUninstalled;
        }
    }
}