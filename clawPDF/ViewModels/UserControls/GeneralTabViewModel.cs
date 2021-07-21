using System;
using System.Collections.Generic;
using zupit.zupitPDF.Core.Settings;
using zupit.zupitPDF.Core.Settings.Enums;
using zupit.zupitPDF.Shared.Helper;
using pdfforge.DynamicTranslator;

namespace zupit.zupitPDF.ViewModels.UserControls
{
    internal class GeneralTabViewModel : ApplicationSettingsViewModel
    {
        private ApplicationProperties _applicationProperties;
        private IList<Language> _languages;

        public GeneralTabViewModel()
        {
            Languages = Translator.FindTranslations(TranslationHelper.Instance.TranslationPath);
        }

        public ApplicationProperties ApplicationProperties
        {
            get => _applicationProperties;

            set
            {
                _applicationProperties = value;
                RaisePropertyChanged("ApplicationProperties");
            }
        }

        public IList<Language> Languages
        {
            get => _languages;
            set
            {
                _languages = value;
                RaisePropertyChanged("Languages");
            }
        }

        public bool DisplayUpdateWarning
        {
            get
            {
                if (ApplicationSettings == null)
                    return false;
                return ApplicationSettings.UpdateInterval == UpdateInterval.Never;
            }
        }

        public IEnumerable<AskSwitchPrinter> AskSwitchPrinterValues =>
            new List<AskSwitchPrinter>
            {
                new AskSwitchPrinter(
                    TranslationHelper.Instance.TranslatorInstance.GetTranslation("ApplicationSettingsWindow", "Ask",
                        "Ask"), true),
                new AskSwitchPrinter(
                    TranslationHelper.Instance.TranslatorInstance.GetTranslation("ApplicationSettingsWindow", "Yes",
                        "Yes"), false)
            };

        public IEnumerable<EnumValue<UpdateInterval>> UpdateIntervals =>
            TranslationHelper.Instance.TranslatorInstance.GetEnumTranslation<UpdateInterval>();

        public bool LanguageIsEnabled => true;

        public string CurrentLanguage
        {
            get => ApplicationSettings.Language;
            set => ApplicationSettings.Language = value;
        }

        protected override void OnSettingsChanged(EventArgs e)
        {
            base.OnSettingsChanged(e);

            RaisePropertyChanged("Languages");
            RaisePropertyChanged("CurrentLanguage");
            RaisePropertyChanged("LanguageIsEnabled");
            RaisePropertyChanged("CurrentUpdateInterval");
            RaisePropertyChanged("UpdateIsEnabled");
        }

        public void UpdateIntervalChanged()
        {
            RaisePropertyChanged("DisplayUpdateWarning");
        }
    }
}