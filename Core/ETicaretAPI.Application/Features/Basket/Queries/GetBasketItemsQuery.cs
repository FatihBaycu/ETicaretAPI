using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Features.Basket.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Basket.Queries
{
    public class GetBasketItemsQuery:IRequest<List<GetBasketItemsQueryResponse>>
    {
    }
    public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQuery, List<GetBasketItemsQueryResponse>>
    {
        readonly IBasketService _basketService;

        public GetBasketItemsQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQuery request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketItemsAsync();
            return basketItems.Select(ba => new GetBasketItemsQueryResponse
            {
               BasketItemId=ba.Id.ToString(),
               Name=ba.Product.Name,
               Price=ba.Product.Price,
               Quantity=ba.Quantity

            }).ToList();
        }
    }
}
