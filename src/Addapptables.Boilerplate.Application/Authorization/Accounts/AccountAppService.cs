using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Extensions;
using Abp.UI;
using Abp.Zero.Configuration;
using Addapptables.Boilerplate.Authorization.Accounts.Dto;
using Addapptables.Boilerplate.Authorization.Impersonation;
using Addapptables.Boilerplate.Authorization.Users;
using Addapptables.Boilerplate.MultiTenancy;
using Addapptables.Boilerplate.Url;
using Addapptables.Boilerplate.Users.Account.Dto;
using Microsoft.AspNetCore.Identity;

namespace Addapptables.Boilerplate.Authorization.Accounts
{
    public class AccountAppService : BoilerplateAppServiceBase, IAccountAppService
    {
        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IWebUrlService _webUrlService;
        private readonly IImpersonationManager _impersonationManager;
        private readonly IUserEmailer _userEmailer;
        public IAppUrlService AppUrlService { get; set; }
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountAppService(
            UserRegistrationManager userRegistrationManager,
            IWebUrlService webUrlService,
            IImpersonationManager impersonationManager,
            IUserEmailer userEmailer,
            IPasswordHasher<User> passwordHasher
            )
        {
            _userRegistrationManager = userRegistrationManager;
            _webUrlService = webUrlService;
            _impersonationManager = impersonationManager;
            AppUrlService = NullAppUrlService.Instance;
            _userEmailer = userEmailer;
            _passwordHasher = passwordHasher;
        }

        [AbpAuthorize(Pages.Tenant.Pages_Tenants_Impersonation)]
        public async Task<ImpersonateOutput> Impersonate(ImpersonateInput input)
        {
            return new ImpersonateOutput
            {
                ImpersonationToken = await _impersonationManager.GetImpersonationToken(input.UserId, input.TenantId),
                TenancyName = await GetTenancyNameOrNullAsync(input.TenantId)
            };
        }


        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id, _webUrlService.GetServerRootAddress(input.TenancyName));
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }

        [AbpAuthorize()]
        public async Task<ImpersonateOutput> BackToImpersonator()
        {
            return new ImpersonateOutput
            {
                ImpersonationToken = await _impersonationManager.GetBackToImpersonatorToken(),
                TenancyName = await GetTenancyNameOrNullAsync(AbpSession.ImpersonatorTenantId)
            };
        }

        private async Task<string> GetTenancyNameOrNullAsync(int? tenantId)
        {
            return tenantId.HasValue ? (await GetActiveTenantAsync(tenantId.Value)).TenancyName : null;
        }

        private async Task<Tenant> GetActiveTenantAsync(int tenantId)
        {
            var tenant = await TenantManager.FindByIdAsync(tenantId);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIdIsNotActive{0}", tenantId));
            }

            return tenant;
        }

        public async Task SendPasswordResetCode(SendPasswordResetCodeDto input)
        {
            var user = await UserManager.FindByNameOrEmailAsync(input.UserNameOrEmail);
            if (user == null)
            {
                throw new UserFriendlyException(L("NoResultsFoundForUser"));
            }
            user.SetNewPasswordResetCode();
            await _userEmailer.SendPasswordResetLinkAsync(user, AppUrlService.CreatePasswordResetUrlFormat(AbpSession.TenantId));
        }

        public async Task AccountResetPassword(AccountResetPasswordDto input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            if (user == null || user.PasswordResetCode.IsNullOrEmpty() || user.PasswordResetCode != input.ResetCode)
            {
                throw new UserFriendlyException(L("InvalidPasswordResetCode"), L("InvalidPasswordResetCode_Detail"));
            }
            user.Password = _passwordHasher.HashPassword(user, input.Password);
            user.PasswordResetCode = null;
            user.IsEmailConfirmed = true;
            await UserManager.UpdateAsync(user);
        }

    }
}
