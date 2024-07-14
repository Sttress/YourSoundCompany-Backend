using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;


namespace YourSoundCompany.EmailService.Service
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IConfiguration _configuration;
        public SendEmailService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public Task SendEmailAsync(List<string> email, string subject, string message)
        {
            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }

        private async Task Execute(List<string> email, string subject, string message)
        {
            try
            {

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_configuration["EmailSettings:Email"])
                };
                
                foreach(var item in email) 
                {
                    mail.To.Add(new MailAddress(item));
                }

                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_configuration["EmailSettings:Domain"], int.Parse(_configuration["EmailSettings:Port"])))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_configuration["EmailSettings:Email"], _configuration["EmailSettings:Password"]);
                    await smtp.SendMailAsync(mail);
                }    

            }catch(Exception ex)
            {
                throw new Exception("Erro ao enviar email",ex);
            }
        }
    }
}
