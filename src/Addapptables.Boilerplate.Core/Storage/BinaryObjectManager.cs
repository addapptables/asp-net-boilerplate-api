using Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.Storage
{
    public class BinaryObjectManager : IBinaryObjectManager
    {
        private readonly IRepository<BinaryObject, Guid> _binaryObjectRepository;

        public BinaryObjectManager(IRepository<BinaryObject, Guid> binaryObjectRepository)
        {
            _binaryObjectRepository = binaryObjectRepository;
        }

        public Task<BinaryObject> GetOrNullAsync(Guid id)
        {
            return _binaryObjectRepository.FirstOrDefaultAsync(id);
        }

        public Task SaveAsync(BinaryObject file)
        {
            return _binaryObjectRepository.InsertAsync(file);
        }

        public Task DeleteAsync(Guid id)
        {
            return _binaryObjectRepository.DeleteAsync(id);
        }
    }
}
