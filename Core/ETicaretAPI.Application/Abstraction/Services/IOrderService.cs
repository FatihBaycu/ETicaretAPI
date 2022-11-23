using ETicaretAPI.Application.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstraction.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDTO creteOrderDTO);
        Task<ListOrderDTO> GetAllOrdersAsync(int page,int size);
        Task<SingleOrder> GetOrderByIdAsync(string id);

    }
}
