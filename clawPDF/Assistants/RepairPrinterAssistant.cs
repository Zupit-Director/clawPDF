using System.Collections.Generic;
using System.IO;
using System.Linq;
using zupit.zupitPDF.Shared.Helper;
using zupit.zupitPDF.Shared.ViewModels;
using zupit.zupitPDF.Shared.Views;
using zupit.zupitPDF.Utilities;
using zupit.zupitPDF.Utilities.IO;
using NLog;
using pdfforge.DynamicTranslator;

namespace zupit.zupitPDF.Assistants
{
    internal class RepairPrinterAssistant
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IAssemblyHelper _assemblyHelper = new AssemblyHelper();
        private readonly IPathSafe _pathSafe = new PathWrapSafe();
        private readonly Translator _translator = TranslationHelper.Instance.TranslatorInstance;

        public bool TryRepairPrinter(IEnumerable<string> printerNames)
        {
            Logger.Error(
                "It looks like the printers are broken. This needs to be fixed to allow zupitPDF to work properly");

            var title = _translator.GetTranslation("Application", "RepairPrinterNoPrintersInstalled",
                "No printers installed");
            var message = _translator.GetFormattedTranslation("Application", "RepairPrinterAskUser",
                "You do not have any zupitPDF printers installed. Most likely there was a problem during the setup or the installation has been altered afterwards.\r\nDo you want to fix this by reinstalling the zupitPDF printers?\r\n\r\nNote: You might be asked twice to grant admin privileges while fixing the problem.");

            Logger.Debug("Asking to start repair..");

            if (MessageWindow.ShowTopMost(message, title, MessageWindowButtons.YesNo, MessageWindowIcon.Exclamation) ==
                MessageWindowResponse.Yes)
            {
                var applicationPath = _assemblyHelper.GetCurrentAssemblyDirectory();
                var printerHelperPath = _pathSafe.Combine(applicationPath, "SetupHelper.exe");

                if (!File.Exists(printerHelperPath))
                {
                    Logger.Error("SetupHelper.exe does not exist!");
                    title = _translator.GetTranslation("Application", "Error", "Error");
                    message = _translator.GetFormattedTranslation("Application", "SetupFileMissing",
                        "An important zupitPDF file is missing ('{0}'). Please reinstall zupitPDF!",
                        _pathSafe.GetFileName(printerHelperPath));

                    MessageWindow.ShowTopMost(message, title, MessageWindowButtons.OK, MessageWindowIcon.Error);
                    return false;
                }

                var shellExecuteHelper = new ShellExecuteHelper();

                //Logger.Debug("Uninstalling Printers...");
                //var uninstallResult = shellExecuteHelper.RunAsAdmin(printerHelperPath, @"/Driver=Remove");
                //Logger.Debug("Done: {0}", uninstallResult);

                Logger.Debug("Reinstalling Printers...");
                var zupitPDFPath = _pathSafe.Combine(applicationPath, "zupitPDF.exe");

                var printerNameString = GetPrinterNameString(printerNames);

                //var installParams = $"/InstallPrinter {printerNameString} /PortApplication \"{zupitPDFPath}\"";
                var installResult = shellExecuteHelper.RunAsAdmin(printerHelperPath, @"/Driver=Add");
                Logger.Debug("Done: {0}", installResult);
            }

            Logger.Debug("Now we'll check again, if the printer is installed");
            if (IsRepairRequired())
            {
                Logger.Info("The printer could not be repaired.");
                title = _translator.GetTranslation("Application", "Error", "Error");
                message = _translator.GetFormattedTranslation("Application", "RepairPrinterFailed",
                    "zupitPDF was not able to repair your printers. Please contact your administrator or the support to assist you in with this problem.");

                MessageWindow.ShowTopMost(message, title, MessageWindowButtons.OK, MessageWindowIcon.Exclamation);
                return false;
            }

            Logger.Info("The printer was repaired successfully");

            return true;
        }

        private string GetPrinterNameString(IEnumerable<string> printerNames)
        {
            var printers = printerNames.ToList();

            if (!printers.Any())
                printers.Add("zupitPDF");

            return string.Join(" ", printers.Select(printerName => "\"" + printerName + "\""));
        }

        public bool IsRepairRequired()
        {
            var printerHelper = new PrinterHelper();
            return !printerHelper.GetzupitPDFPrinters().Any();
        }
    }
}