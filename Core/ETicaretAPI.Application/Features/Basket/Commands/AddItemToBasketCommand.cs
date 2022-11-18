using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Features.Basket.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Basket.Commands
{
    public class AddItemToBasketCommand:IRequest<AddItemToBasketCommandResponse>
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommand, AddItemToBasketCommandResponse>
    {
        readonly IBasketService _basketService;

        public AddItemToBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
        {
            await _basketService.AddItemToBasketAsync(new()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            });
            return new();
        }
    }
}
