﻿using System.Collections.Generic;
using zupit.zupitPDF.Core.Jobs;
using zupit.zupitPDF.Core.Settings.Enums;
using zupit.zupitPDF.Utilities;
using SystemInterface.IO;

namespace zupit.zupitPDF.Core.Ghostscript.OutputDevices
{
    /// <summary>
    ///     Extends OutputDevice to create PNG files
    /// </summary>
    public class JpegDevice : OutputDevice
    {
        public JpegDevice(IJob job) : base(job)
        {
        }

        public JpegDevice(IJob job, IFile file, IOsHelper osHelper) : base(job, file, osHelper)
        {
        }

        protected override void AddDeviceSpecificParameters(IList<string> parameters)
        {
            switch (Job.Profile.JpegSettings.Color)
            {
                case JpegColor.Gray8Bit:
                    parameters.Add("-sDEVICE=jpeggray");
                    break;

                default:
                    parameters.Add("-sDEVICE=jpeg");
                    break;
            }

            parameters.Add("-dJPEGQ=" + Job.Profile.JpegSettings.Quality);
            parameters.Add("-r" + Job.Profile.JpegSettings.Dpi);
            parameters.Add("-dTextAlphaBits=4");
            parameters.Add("-dGraphicsAlphaBits=4");
        }

        protected override string ComposeOutputFilename()
        {
            //%d for multiple Pages
            return Job.JobTempFileName + "%d.jpg";
        }
    }
}