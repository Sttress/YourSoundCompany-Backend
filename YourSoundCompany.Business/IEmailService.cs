using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompany.Business
{
    public interface IEmailService
    {
        Task VerificationEmail(string email, string name, int codeVerify);
        Task RecoveryPasswordEmail(string email, string name, string CodeToUrl);
    }
}
