using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Features.Basket.Models;
using MediatR;

namespace ETicaretAPI.Application.Features.Basket.Commands
{
    public class UpdateQuantityCommand : IRequest<UpdateQuantityCommandResponse>
    {
        public string BasketItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommand, UpdateQuantityCommandResponse>
    {
        readonly IBasketService _basketService;

        public UpdateQuantityCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<UpdateQuantityCommandResponse> Handle(UpdateQuantityCommand request, CancellationToken cancellationToken)
        {
            await _basketService.UpdateQuantityAsync(new()
            {
                BasketItemId = request.BasketItemId,
                Quantity = request.Quantity,
            });
            return new();
        }
    }
}
