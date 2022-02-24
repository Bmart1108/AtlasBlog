using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Security;

namespace AtlasBlog.Services
{
    public class BasicEmailService : IEmailSender
    {

        private readonly IConfiguration _appSettings;

        public BasicEmailService(IConfiguration appSettings)
        {
            _appSettings = appSettings;

        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //We need to compose an Email based partially on the data supplied by the user
            MimeMessage newEmail = new();
            var emailSender = _appSettings["SmtpSettings:UserName"];


            newEmail.Sender = MailboxAddress.Parse(emailSender);
            newEmail.To.Add(MailboxAddress.Parse(email));
            newEmail.Subject = subject;

            //The body of the email is a bit more tricky
            BodyBuilder emailBody = new();
            emailBody.HtmlBody = htmlMessage;
            newEmail.Body = emailBody.ToMessageBody();
            //We are done with the body of the email

            //Now, we need to configure the SMTP server
            using SmtpClient smtpClient = new();

            var host = _appSettings["SmtpSettings:Host"];
            var port = Convert.ToInt32(_appSettings["SmtpSettings:Port"]);


            await smtpClient.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtpClient.AuthenticateAsync(emailSender, _appSettings["SmtpSettings:Password"]);
            await smtpClient.SendAsync(newEmail);
            await smtpClient.DisconnectAsync(true);
        }

    }

}
