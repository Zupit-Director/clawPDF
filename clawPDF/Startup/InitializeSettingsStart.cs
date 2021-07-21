using zupit.zupitPDF.Helper;

namespace zupit.zupitPDF.Startup
{
    internal class InitializeSettingsStart : IAppStart
    {
        public bool Run()
        {
            SettingsHelper.SaveSettings();

            return true;
        }
    }
}