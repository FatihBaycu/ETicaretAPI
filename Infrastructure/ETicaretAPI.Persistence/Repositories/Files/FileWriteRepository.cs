using ETicaretAPI.Application.Repositories.Files;
using ETicaretAPI.Persistence.Contexts;
using File = ETicaretAPI.Domain.Entities.File;

namespace ETicaretAPI.Persistence.Repositories.Files
{
    public class FileWriteRepository : WriteRepository<File>, IFileWriteRepository
    {
        public FileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
