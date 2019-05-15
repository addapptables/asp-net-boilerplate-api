using Abp.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.Storage
{
    public interface IBinaryObjectManager : IDomainService
    {
        Task<BinaryObject> GetOrNullAsync(Guid id);

        Task SaveAsync(BinaryObject file);

        Task DeleteAsync(Guid id);
    }
}
