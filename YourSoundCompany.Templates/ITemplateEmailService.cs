using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourSoundCompany.Templates.Enum;

namespace YourSoundCompany.Templates
{
    public interface ITemplateEmailService
    {
        Task<string> RenderTemplate(TemplateEmailEnum template, object obj);
    }
}
