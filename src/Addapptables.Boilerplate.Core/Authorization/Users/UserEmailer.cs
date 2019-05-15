using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Net.Mail;
using Abp.UI;
using Addapptables.Boilerplate.Emailing;
using Addapptables.Boilerplate.MultiTenancy;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.Authorization.Users
{
    public class UserEmailer : AddapptablesDomainServiceBase, IUserEmailer
    {
        private readonly IEmailTemplateProvider _emailTemplateProvider;

        private readonly ICurrentUnitOfWorkProvider _unitOfWorkProvider;

        private readonly IRepository<Tenant> _tenantRepository;

        private readonly IEmailSender _emailSender;

        public UserEmailer(
            IEmailTemplateProvider emailTemplateProvider,
            ICurrentUnitOfWorkProvider unitOfWorkProvider,
            IRepository<Tenant> tenantRepository,
            IEmailSender emailSender
            )
        {
            _emailTemplateProvider = emailTemplateProvider;
            _unitOfWorkProvider = unitOfWorkProvider;
            _tenantRepository = tenantRepository;
            _emailSender = emailSender;
        }

        public async Task SendPasswordResetLinkAsync(User user, string link = null)
        {
            if (user.PasswordResetCode.IsNullOrEmpty())
            {
                throw new Exception("PasswordResetCode should be set in order to send password reset link.");
            }
            var tenancyName = GetTenancyNameOrNull(user.TenantId);
            var emailTemplate = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(user.TenantId));
            emailTemplate.Replace("{EMAIL_TITLE}", this.L("RecoverPassword"));
            emailTemplate.Replace("{EMAIL_SUB_TITLE}", this.L("RecoverPasswordSubtitle"));
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<b>" + L("NameSurname") + "</b>: " + user.Name + " " + user.Surname + "<br />");

            if (!tenancyName.IsNullOrEmpty())
            {
                mailMessage.AppendLine("<b>" + L("TenancyName") + "</b>: " + tenancyName + "<br />");
            }

            mailMessage.AppendLine("<b>" + L("UserName") + "</b>: " + user.UserName + "<br />");
            mailMessage.AppendLine("<b>" + L("ResetCode") + "</b>: " + user.PasswordResetCode + "<br />");

            if (!link.IsNullOrEmpty())
            {
                link = link.Replace("{userId}", user.Id.ToString());
                link = link.Replace("{resetCode}", Uri.EscapeDataString(user.PasswordResetCode));

                if (user.TenantId.HasValue)
                {
                    link = link.Replace("{tenantId}", user.TenantId.ToString());
                }

                mailMessage.AppendLine("<br />");
                mailMessage.AppendLine(L("PasswordResetEmail_ClickTheLinkBelowToResetYourPassword") + "<br /><br />");
                mailMessage.AppendLine("<a href=\"" + link + "\">" + link + "</a>");
            }
            await ReplaceBodyAndSend(user.EmailAddress, L("PasswordResetEmail_Subject"), emailTemplate, mailMessage);
        }

        private async Task ReplaceBodyAndSend(string emailAddress, string subject, StringBuilder emailTemplate, StringBuilder mailMessage)
        {
            emailTemplate.Replace("{EMAIL_BODY}", mailMessage.ToString());
            try
            {
                await _emailSender.SendAsync(new MailMessage
                {
                    To = { emailAddress },
                    Subject = subject,
                    Body = emailTemplate.ToString(),
                    IsBodyHtml = true
                });
            }
            catch (Exception ex)
            {

                throw new UserFriendlyException(string.Format(L("ErrorToSendEmail"), ex.Message));
            }
        }

        private string GetTenancyNameOrNull(int? tenantId)
        {
            if (tenantId == null)
            {
                return null;
            }

            using (_unitOfWorkProvider.Current.SetTenantId(null))
            {
                return _tenantRepository.Get(tenantId.Value).TenancyName;
            }
        }
    }
}
