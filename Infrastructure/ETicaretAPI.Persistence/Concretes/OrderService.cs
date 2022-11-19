using AutoMapper;
using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.DTOs.Orders;
using ETicaretAPI.Application.Repositories.Orders;
using ETicaretAPI.Domain.Entities;
using MediatR;
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

        public OrderService(IOrderWriteRepository orderWriteRepository, IMapper mapper)
        {
            _orderWriteRepository = orderWriteRepository;
            _mapper = mapper;
        }

        public async Task CreateOrderAsync(CreateOrderDTO creteOrderDTO)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Address = creteOrderDTO.Address,
                Id = Guid.Parse(creteOrderDTO.BasketId),
                Description = creteOrderDTO.Description
            });
            await _orderWriteRepository.SaveAsync();

        }
    }
}
