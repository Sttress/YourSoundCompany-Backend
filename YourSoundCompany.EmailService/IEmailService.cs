using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompany.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(List<string> email, string subject, string message);
    }
}
