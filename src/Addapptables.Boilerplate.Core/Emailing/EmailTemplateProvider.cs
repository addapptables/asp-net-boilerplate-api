using Abp.Dependency;
using Abp.IO.Extensions;
using Abp.Reflection.Extensions;
using System;
using System.Text;

namespace Addapptables.Boilerplate.Emailing
{
    public class EmailTemplateProvider : IEmailTemplateProvider, ITransientDependency
    {
        public string GetDefaultTemplate(int? tenantId)
        {
            var template = "";
            using (var stream = typeof(EmailTemplateProvider).GetAssembly().GetManifestResourceStream("Addapptables.Boilerplate.Emailing.Templates.default.html"))
            {
                var bytes = stream.GetAllBytes();
                template = Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
            }
            template = template.Replace("{THIS_YEAR}", DateTime.Now.Year.ToString());
            return template;
        }
    }
}
