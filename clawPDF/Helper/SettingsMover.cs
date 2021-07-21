using System.Security;
using zupit.zupitPDF.Utilities.Registry;
using SystemInterface.Microsoft.Win32;
using SystemWrapper.Microsoft.Win32;

namespace zupit.zupitPDF.Helper
{
    /// <summary>
    ///     It is good practice to store Registry settings under HKEY_CURRENT_USER\Software\CompanyName\ProductName
    ///     In the past, we stored them under HKEY_CURRENT_USER\Software\zupitPDF.net
    ///     They are now stored under HKEY_CURRENT_USER\Software\clawSoft\zupitPDF
    ///     This class checks if old settings exist, if they need to be moved to the new location
    ///     and performs the move if required.
    /// </summary>
    public class SettingsMover
    {
        private const string OldRegistryPath = @"Software\zupitPDF.Net";
        private const string NewRegistryPath = @"Software\clawSoft\zupitPDF";
        private readonly RegistryUtility _registryUtility;
        private readonly IRegistry _registryWrap;

        public SettingsMover()
            : this(new RegistryWrap(), new RegistryUtility())
        {
        }

        public SettingsMover(IRegistry registryWrap, RegistryUtility registryUtility)
        {
            _registryWrap = registryWrap;
            _registryUtility = registryUtility;
        }

        public bool MoveRequired()
        {
            try
            {
                var regKey = _registryWrap.CurrentUser.OpenSubKey(OldRegistryPath);
                if (regKey == null)
                    return false;

                regKey.Close();

                regKey = _registryWrap.CurrentUser.OpenSubKey(NewRegistryPath);
                if (regKey == null)
                    return true;

                regKey.Close();
            }
            catch (SecurityException)
            {
            }

            return false;
        }

        public bool MoveSettings()
        {
            if (!MoveRequired())
                return false;

            return _registryUtility.RenameSubKey(_registryWrap.CurrentUser, OldRegistryPath, NewRegistryPath);
        }
    }
}