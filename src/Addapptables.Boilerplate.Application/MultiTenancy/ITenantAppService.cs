using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Addapptables.Boilerplate.MultiTenancy.Dto;

namespace Addapptables.Boilerplate.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, UpdateTenantDto, TenantDto>
    {
    }
}

