using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public class SendEmailProcessHandler : ProcessHandler
    {
        private readonly string _fileName;
        private readonly string _toEmail;
        private readonly string _subject;
        private readonly string _body;

        public SendEmailProcessHandler(string fileName, string toEmail, string subject, string body)
        {
            _fileName = fileName;
            _toEmail = toEmail;
            _subject = subject;
            _body = body;
        }

        public override object Handle(object processHandler)
        {
            var memoryStream = processHandler as MemoryStream;
            memoryStream.Position = 0;

            var mailMessage = new MailMessage
            {
                From = new MailAddress("deneme@deneme.com")
            };

            mailMessage.To.Add(new MailAddress(_toEmail));
            mailMessage.Subject = _subject;
            mailMessage.Body = _body;
            mailMessage.IsBodyHtml = true;

            Attachment attachment = new Attachment(memoryStream, _fileName, MediaTypeNames.Application.Zip);

            mailMessage.Attachments.Add(attachment);

            var smptClient = new SmtpClient("server");
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential("deneme@deneme.com", "deneme");
            //smptClient.Send(mailMessage);


            return base.Handle(processHandler);
        }
    }
}
