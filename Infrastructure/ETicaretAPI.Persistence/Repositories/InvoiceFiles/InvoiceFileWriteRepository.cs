using ETicaretAPI.Application.Repositories.InvoiceFiles;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.InvoiceFiles
{
    public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
