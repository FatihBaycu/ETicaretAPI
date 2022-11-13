using ETicaretAPI.Application.Repositories.Customers;
using ETicaretAPI.Application.Repositories.Files;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = ETicaretAPI.Domain.Entities.File;

namespace ETicaretAPI.Persistence.Repositories.Files
{
    public class FileReadRepository : ReadRepository<File>, IFileReadRepository
    {
        public FileReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
