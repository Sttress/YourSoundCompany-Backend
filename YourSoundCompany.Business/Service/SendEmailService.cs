using Microsoft.Extensions.Configuration;
using YourSoundCompany.EmailService;
using YourSoundCompany.Templates;
using YourSoundCompany.Templates.Enum;

namespace YourSoundCompany.Business.Service
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IEmailService _emailService;
        private readonly ITemplateEmailService _templateEmailService;
        private readonly IConfiguration _configuration;


        public SendEmailService
            (
                IEmailService emailService,
                ITemplateEmailService templateEmailService,
                IConfiguration configuration
            ) 
        {
            _emailService = emailService;
            _templateEmailService = templateEmailService;
            _configuration = configuration;
        }

        public async Task RecoveryPasswordEmail(string email, string name, string CodeToUrl)
        {
            try
            {
                object obj = new
                {
                    userName = name,
                    url = $@"{_configuration["URLs:FrontEnd"]}recovery-password?code={CodeToUrl}"
                };

                var listEmail = new List<string>();
                listEmail.Add(email);

                var body = await _templateEmailService.RenderTemplate(TemplateEmailEnum.RecoveryPassword, obj);
                await _emailService.SendEmailAsync(listEmail, "Recuperação de Email", body);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task VerificationEmail(string email, string name, int codeVerify)
        {
            try
            {
                object obj = new
                {
                    userName = name,
                    code = codeVerify
                };

                var listEmail = new List<string>();
                listEmail.Add(email);

                var body = await _templateEmailService.RenderTemplate(TemplateEmailEnum.VerifyEmail, obj);
                await _emailService.SendEmailAsync(listEmail, "Código de Verificação", body);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
