using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Addapptables.Boilerplate.Editions.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.Editions
{
    public class EditionAppService : AsyncCrudAppService<FeaturesEdition, EditionDto, int, GetEditionDto, CreateEditionDto, UpdateEditionDto>, IEditionAppService
    {
        private readonly EditionManager _editionManager;

        public EditionAppService(IRepository<FeaturesEdition> editionRepository, EditionManager editionManager)
            : base(editionRepository)
        {
            _editionManager = editionManager;
            GetAllPermissionName = Authorization.Pages.Edition.Pages_Editions;
            UpdatePermissionName = Authorization.Pages.Edition.Pages_Editions_Edit;
            CreatePermissionName = Authorization.Pages.Edition.Pages_Editions_Create;
            DeletePermissionName = Authorization.Pages.Edition.Pages_Editions_Delete;
        }

        public override async Task<EditionDto> Create(CreateEditionDto input)
        {
            CheckCreatePermission();
            var edition = ObjectMapper.Map<FeaturesEdition>(input);

            edition.DisplayName = edition.Name;

            if (!input.Price.HasValue)
            {
                edition.IsFree = true;
            }
            else
            {
                edition.IsFree = false;
            }

            await _editionManager.CreateAsync(edition);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<EditionDto>(edition);
        }

        [AbpAuthorize(Authorization.Pages.Edition.Pages_Editions, Authorization.Pages.Tenant.Pages_Tenants)]
        public async Task<IList<EditionMinimalDto>> GetAllEditionMinimal()
        {
            var editions = await Repository.GetAllListAsync();
            return ObjectMapper.Map<IList<EditionMinimalDto>>(editions);
        }

        public override async Task<EditionDto> Update(UpdateEditionDto input)
        {
            CheckUpdatePermission();
            var editionBD = await Repository.GetAsync(input.Id);

            ObjectMapper.Map(input, editionBD);

            if (!input.Price.HasValue)
            {
                editionBD.IsFree = true;
            }
            else
            {
                editionBD.IsFree = false;
            }
            await Repository.UpdateAsync(editionBD);

            return ObjectMapper.Map<EditionDto>(editionBD);
        }
    }
}
