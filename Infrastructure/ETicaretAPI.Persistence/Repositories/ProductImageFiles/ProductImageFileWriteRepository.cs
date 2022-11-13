using ETicaretAPI.Application.Repositories.ProductImageFiles;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.ProductImageFiles
{
    public class ProductImageFileWriteRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
