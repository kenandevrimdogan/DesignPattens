using BasePoject.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;

namespace WebApp.Observer.Observer
{
    public class UserObserverSendEmail : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverSendEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateUser(AppUser appUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();

            var mailMessage = new MailMessage
            {
                From = new MailAddress("deneme@deneme.com")
            };

            mailMessage.To.Add(new MailAddress(appUser.Email));
            mailMessage.Subject = "Welcome";
            mailMessage.Body = "<p>Our Web Site</p>";
            mailMessage.IsBodyHtml = true;

            var smptClient = new SmtpClient("server");
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential("deneme@deneme.com", "deneme");
            //smptClient.Send(mailMessage);

            logger.LogInformation($"Email was send to user: { appUser.UserName }");
        }
    }
}
