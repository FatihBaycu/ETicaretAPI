using ETicaretAPI.Application.Abstraction.Hubs;
using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Features.Orders.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Orders.Commands
{
    public class CreateOrderCommand : IRequest<CreateOrderCommandResponse>
    {
        public string Description { get; set; }
        public string Address { get; set; }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;
        readonly IOrderHubService _orderHubService;

        public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderService.CreateOrderAsync(new()
            {
                Description = request.Description,
                Address = request.Address,
                BasketId=_basketService.GetUserActiveBasket?.Id.ToString()
            });
            
            await _orderHubService.OrderAddedMessageAsync("came order has arrived. :) ");
            //todo error.
            return new();
        }
    }
}
