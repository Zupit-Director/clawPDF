using System;
using zupit.zupitPDF.Core.Jobs;

namespace zupit.zupitPDF
{
    /// <summary>
    ///     EventArgs class that contains the new JobInfo
    /// </summary>
    public class NewJobInfoEventArgs : EventArgs
    {
        public NewJobInfoEventArgs(IJobInfo jobInfo)
        {
            JobInfo = jobInfo;
        }

        public IJobInfo JobInfo { get; }
    }
}