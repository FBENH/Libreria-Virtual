namespace API.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string fromAddress,
                            string destinationAddress,
                            string subject,
                            string textMessage);
    }
}
