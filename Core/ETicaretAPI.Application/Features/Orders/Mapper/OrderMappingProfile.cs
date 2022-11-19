using AutoMapper;
using ETicaretAPI.Application.DTOs.Orders;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Orders.Mapper
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<CreateOrderDTO, Order>().ReverseMap();
            CreateMap<Order, CreateOrderDTO>().ForMember(dest=>dest.BasketId,opt=>opt.MapFrom(src=>(Domain.Entities.Basket)src.Basket));
        }
    }
}
