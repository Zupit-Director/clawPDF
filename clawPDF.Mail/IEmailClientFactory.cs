namespace zupit.zupitPDF.Mail
{
    public interface IEmailClientFactory
    {
        IEmailClient CreateEmailClient();
    }
}