using AutoMapper;
using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.DTOs.Orders;
using ETicaretAPI.Application.Repositories.Orders;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Concretes
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IMapper _mapper;
        readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IMapper mapper, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _mapper = mapper;
            _orderReadRepository = orderReadRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDTO creteOrderDTO)
        {
            var orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);
            await _orderWriteRepository.AddAsync(new()
            {
                Address = creteOrderDTO.Address,
                Id = Guid.Parse(creteOrderDTO.BasketId),
                Description = creteOrderDTO.Description,
                OrderCode = orderCode

            });
            await _orderWriteRepository.SaveAsync();

        }

        public async Task<ListOrderDTO> GetAllOrdersAsync(int page, int size)
        {

            var query = _orderReadRepository.Table.Include(x => x.Basket)
                  .ThenInclude(c => c.User)
                  .Include(a => a.Basket)
                  .ThenInclude(b => b.BasketItems)
                  .ThenInclude(d => d.Product)
                  .Select(o=> new
                  {
                      CreatedDate = o.CreatedDate,
                      OrderCode = o.OrderCode,
                      TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                      UserName = o.Basket.User.UserName
                  });

            var data = query.Skip(page * size).Take(size);
            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders=data
            };
        }
    }
}
