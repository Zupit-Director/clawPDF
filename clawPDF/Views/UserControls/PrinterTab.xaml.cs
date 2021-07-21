using System.Windows;
using System.Windows.Controls;
using zupit.zupitPDF.Shared.Assistants;
using zupit.zupitPDF.Shared.Helper;
using zupit.zupitPDF.Utilities;
using zupit.zupitPDF.ViewModels.UserControls;
using zupit.zupitPDF.ViewModels.Wrapper;

namespace zupit.zupitPDF.Views.UserControls
{
    internal partial class PrinterTab
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;
        private readonly PrinterHelper _printerHelper = new PrinterHelper();

        public PrinterTab()
        {
            InitializeComponent();
            if (TranslationHelper.IsInitialized) TranslationHelper.TranslatorInstance.Translate(this);
            ViewModel.AddPrinterAction = AddPrinterAction;
            ViewModel.DeletePrinterAction = DeletePrinterAction;
        }

        public PrinterTabViewModel ViewModel
        {
            get => (PrinterTabViewModel)DataContext;
            set => DataContext = value;
        }

        public Visibility RequiresUacVisibility =>
            new OsHelper().UserIsAdministrator() ? Visibility.Collapsed : Visibility.Visible;

        private void PrimaryPrinterBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel == null)
                return;

            ViewModel.UpdatePrimaryPrinter(ViewModel.ApplicationSettings.PrimaryPrinter);
        }

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Cancel = true;
        }

        private string AddPrinterAction()
        {
            var helper = new PrinterActionsAssistant();

            string printerName;
            helper.AddPrinter(out printerName);

            return printerName;
        }

        private void DeletePrinterAction(PrinterMappingWrapper printerMapping)
        {
            var helper = new PrinterActionsAssistant();
            var success = helper.DeletePrinter(printerMapping.PrinterName, ViewModel.zupitPDFPrinters.Count);

            if (success)
            {
                ViewModel.PrinterMappings.Remove(printerMapping);
                ViewModel.zupitPDFPrinters = _printerHelper.GetzupitPDFPrinters();
                PrimaryPrinterBox.SelectedValue = ViewModel.PrimaryPrinter;
            }
        }

        public void UpdateProfilesList()
        {
            ViewModel.RefreshPrinterMappings();
        }
    }
}