using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Features.Orders.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Orders.Queries
{
    public class GetOrderByIdQuery:IRequest<GetOrderByIdQueryResponse>
    {
        public string Id { get; set; }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var singleOrder=await _orderService.GetOrderByIdAsync(request.Id);
            return new(){
            Address=singleOrder.Address,
            BasketItems=singleOrder.BasketItems,
            CreatedDate=singleOrder.CreatedDate,
            Description=singleOrder.Description,
            Id=singleOrder.Id,
            OrderCode = singleOrder.OrderCode,
            };
        }
    }
}
