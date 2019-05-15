using System.Threading.Tasks;
using Abp.Application.Services;
using Addapptables.Boilerplate.Sessions.Dto;

namespace Addapptables.Boilerplate.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
