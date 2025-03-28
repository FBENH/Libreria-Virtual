using Microsoft.AspNetCore.Identity.UI.Services;

namespace API.Services
{
    public class EmailSender : IEmailSender
    {        
        public async Task SendEmailAsync(string fromAddress, string destinationAddress, string subject, string textMessage)
        {
            //sending email
            await Task.CompletedTask;
        }
    }
}
