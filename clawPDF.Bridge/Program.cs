using System.IO;
using System.Windows.Forms;

namespace zupitPDF.Bridge
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args != null || args.Length != 0)
            {
                try
                {
                    string inffile = args[0].Split('=')[1];
                    if (File.Exists(inffile)) start(inffile);
                }
                catch
                {
                }
            }
        }

        private static void start(string infFile)
        {
            INIFile iniFile = new INIFile(infFile);
            string username = iniFile.Read("0", "Username");
            ProcessExtensions.StartProcessAsUser(username, Path.GetDirectoryName(Application.ExecutablePath) + @"\" + "zupitPDF.exe", Path.GetDirectoryName(Application.ExecutablePath) + @"\" + "zupitPDF.exe" + " /INFODATAFILE=" + infFile, Path.GetDirectoryName(Application.ExecutablePath), true);
        }
    }
}