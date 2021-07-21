using System.Windows;
using zupit.zupitPDF.Core.Settings;
using zupit.zupitPDF.Helper;
using zupit.zupitPDF.Shared.Helper;
using zupit.zupitPDF.ViewModels;
using zupit.zupitPDF.WindowsApi;

namespace zupit.zupitPDF.Views
{
    internal partial class PrintJobWindow
    {
        private zupitPDFSettings _settings = SettingsHelper.Settings;

        public PrintJobWindow()
        {
            InitializeComponent();
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = (PrintJobViewModel)DataContext;

            TopMostHelper.UndoTopMostWindow(this);
            _settings.ApplicationSettings.LastUsedProfileGuid = vm.SelectedProfile.Guid;

            var window = new ProfileSettingsWindow(_settings);
            if (window.ShowDialog() == true)
            {
                _settings = window.Settings;

                vm.Profiles = _settings.ConversionProfiles;
                vm.ApplicationSettings = _settings.ApplicationSettings;
                vm.SelectProfileByGuid(_settings.ApplicationSettings.LastUsedProfileGuid);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
            FlashWindow.Flash(this, 3);
        }

        private void CommandButtons_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            DragAndDropHelper.DragEnter(e);
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            DragAndDropHelper.Drop(e);
        }
    }
}