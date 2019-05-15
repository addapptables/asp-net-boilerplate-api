using System.Threading.Tasks;
using Abp.Application.Services;
using Addapptables.Boilerplate.Authorization.Accounts.Dto;
using Addapptables.Boilerplate.Users.Account.Dto;

namespace Addapptables.Boilerplate.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);

        Task<ImpersonateOutput> Impersonate(ImpersonateInput input);

        Task<ImpersonateOutput> BackToImpersonator();

        Task SendPasswordResetCode(SendPasswordResetCodeDto input);

        Task AccountResetPassword(AccountResetPasswordDto input);
    }
}
