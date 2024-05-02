using HandlebarsDotNet;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YourSoundCompany.Templates.Enum;

namespace YourSoundCompany.Templates.Service
{
    public class TemplateEmailService :ITemplateEmailService
    {
        private static Dictionary<string, HandlebarsTemplate<object, object>> _cacheTemplate = new();

        public TemplateEmailService() { }

        public async Task<string> RenderTemplate(TemplateEmailEnum template, object obj)
        {
            var key = template.ToString();
            if (!_cacheTemplate.ContainsKey(key))
            {
                var templateString = await GetTemplateContent(template);
                _cacheTemplate.Add(key, Handlebars.Compile(templateString));
            }

            return _cacheTemplate[key](obj);
        }

        private static async Task<string> GetTemplateContent(TemplateEmailEnum template)
        {
            var templateName = GetTemplateFileName(template);

            var embeddedFileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            var fileInfo = embeddedFileProvider.GetFileInfo($"Template.Email.{templateName}");

            using var stream = fileInfo.CreateReadStream();
            using var reader = new StreamReader(stream, Encoding.UTF8);

            return await reader.ReadToEndAsync();
        }

        private static string GetTemplateFileName(TemplateEmailEnum template) => template switch
        {
            TemplateEmailEnum.VerifyEmail => "verify-email.html",
            TemplateEmailEnum.RecoveryPassword => "recovery-password.html",
            _ => throw new Exception(@$"""{template}"" Template file name not found.")
        };
    }
}
