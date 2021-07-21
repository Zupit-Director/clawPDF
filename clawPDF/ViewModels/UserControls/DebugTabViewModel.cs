using System;
using System.Collections.Generic;
using zupit.zupitPDF.Core.Settings.Enums;
using zupit.zupitPDF.Shared.Helper;
using pdfforge.DynamicTranslator;

namespace zupit.zupitPDF.ViewModels.UserControls
{
    internal class DebugTabViewModel : ApplicationSettingsViewModel
    {
        public IEnumerable<EnumValue<LoggingLevel>> LoggingValues =>
            TranslationHelper.Instance.TranslatorInstance.GetEnumTranslation<LoggingLevel>();

        public bool ProfileManagementIsEnabled => true;

        protected override void OnSettingsChanged(EventArgs e)
        {
            base.OnSettingsChanged(e);

            RaisePropertyChanged("ProfileManagementIsEnabled");
        }
    }
}