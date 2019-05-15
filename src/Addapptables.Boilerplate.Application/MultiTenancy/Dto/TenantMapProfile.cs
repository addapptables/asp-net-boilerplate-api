using Addapptables.Boilerplate.MultiTenancy.Rules.Models;
using AutoMapper;

namespace Addapptables.Boilerplate.MultiTenancy.Dto
{
    public class TenantMapProfile : Profile
    {
        public TenantMapProfile()
        {
            CreateMap<CreateTenantDto, TenantModel>();
            CreateMap<UpdateTenantDto, TenantModel>();
        }
    }
}
