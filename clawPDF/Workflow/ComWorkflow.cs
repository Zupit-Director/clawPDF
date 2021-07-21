﻿using System;
using System.Runtime.InteropServices;
using zupit.zupitPDF.Core.Actions;
using zupit.zupitPDF.Core.Jobs;
using zupit.zupitPDF.Core.Settings;
using zupit.zupitPDF.Shared.Helper;

namespace zupit.zupitPDF.Workflow
{
    internal class ComWorkflow : ConversionWorkflow
    {
        public ComWorkflow(IJob job, zupitPDFSettings settings)
        {
            JobInfo = job.JobInfo;
            Job = job;
            Settings = settings;

            SetJobSettings();
        }

        private void SetJobSettings()
        {
            Job.AutoCleanUp = true;
        }

        protected override void QueryTargetFile()
        {
        }

        protected override bool QueryEncryptionPasswords()
        {
            Job.Passwords.PdfOwnerPassword = Job.Profile.PdfSettings.Security.OwnerPassword;
            Job.Passwords.PdfUserPassword = Job.Profile.PdfSettings.Security.UserPassword;

            return true;
        }

        protected override bool QuerySignaturePassword()
        {
            Job.Passwords.PdfSignaturePassword = Job.Profile.PdfSettings.Signature.SignaturePassword;

            return true;
        }

        protected override bool QueryEmailSmtpPassword()
        {
            Job.Passwords.SmtpPassword = Job.Profile.EmailSmtp.Password;

            return true;
        }

        protected override bool QueryFtpPassword()
        {
            Job.Passwords.FtpPassword = Job.Profile.Ftp.Password;

            return true;
        }

        protected override void NotifyUserAboutFailedJob()
        {
        }

        protected override bool EvaluateActionResult(ActionResult actionResult)
        {
            if (actionResult.Success)
                return true;

            throw new COMException(ErrorCodeInterpreter.GetErrorText(actionResult[0], true));
        }

        protected override void RetypeSmtpPassword(object sender, QueryPasswordEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}