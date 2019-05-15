using Abp.Application.Services;
using Addapptables.Boilerplate.Editions.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.Editions
{
    public interface IEditionAppService : IAsyncCrudAppService<EditionDto, int, GetEditionDto, CreateEditionDto, UpdateEditionDto>
    {
        Task<IList<EditionMinimalDto>> GetAllEditionMinimal();
    }
}
