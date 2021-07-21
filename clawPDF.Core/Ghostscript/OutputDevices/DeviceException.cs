using System;

namespace zupit.zupitPDF.Core.Ghostscript.OutputDevices
{
    public class DeviceException : Exception
    {
        public DeviceException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; }
    }
}