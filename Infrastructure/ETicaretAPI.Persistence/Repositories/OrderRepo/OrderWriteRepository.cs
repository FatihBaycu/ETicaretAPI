using ETicaretAPI.Application.Repositories.Orders;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.OrderRepo
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
