using System.Windows.Controls;
using zupit.zupitPDF.Shared.Helper;
using zupit.zupitPDF.Shared.ViewModels.UserControls;

namespace zupit.zupitPDF.Shared.Views.UserControls
{
    public partial class ActionsTab : UserControl
    {
        public ActionsTab()
        {
            InitializeComponent();
            if (TranslationHelper.Instance.IsInitialized) TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        public ActionsTabViewModel ViewModel => (ActionsTabViewModel)DataContext;
    }
}