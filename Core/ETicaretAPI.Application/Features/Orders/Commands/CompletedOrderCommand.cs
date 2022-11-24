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
    public class CompletedOrderCommand:IRequest<CompletedOrderCommandResponse>
    {
        public string Id { get; set; }
    }
    public class CompletedOrderCommandHandler : IRequestHandler<CompletedOrderCommand, CompletedOrderCommandResponse>
    {
        readonly IOrderService _orderService;

        public CompletedOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<CompletedOrderCommandResponse> Handle(CompletedOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderService.CompleteOrderAsync(request.Id);
            return new();
        }
    }
}
