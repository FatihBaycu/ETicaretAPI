using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Features.Basket.Models;
using MediatR;

namespace ETicaretAPI.Application.Features.Basket.Commands
{
    public class RemoveBasketItemCommand:IRequest<RemoveBasketItemCommandResponse>
    {
        public string BasketItemId { get; set; }
    }
    public class RemoveBasketItemCommandHandler : IRequestHandler<RemoveBasketItemCommand, RemoveBasketItemCommandResponse>
    {
        readonly IBasketService _basketService;

        public RemoveBasketItemCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<RemoveBasketItemCommandResponse> Handle(RemoveBasketItemCommand request, CancellationToken cancellationToken)
        {
            await _basketService.RemoveBasketItemAsync(request.BasketItemId);
            return new();
        }
    }
}
