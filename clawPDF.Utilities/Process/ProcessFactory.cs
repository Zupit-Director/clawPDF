using System.Diagnostics;

namespace zupit.zupitPDF.Utilities.Process
{
    public class ProcessWrapperFactory
    {
        public virtual ProcessWrapper BuildProcessWrapper(ProcessStartInfo startInfo)
        {
            return new ProcessWrapper(startInfo);
        }
    }
}