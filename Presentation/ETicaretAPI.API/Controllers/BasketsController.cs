using ETicaretAPI.Application.Features.Basket.Commands;
using ETicaretAPI.Application.Features.Basket.Models;
using ETicaretAPI.Application.Features.Basket.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    namespace ETicaretAPI.API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        [Authorize(AuthenticationSchemes = "Admin")]
        public class BasketsController : ControllerBase
        {
            readonly IMediator _mediator;

            public BasketsController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpGet]
            public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQuery getBasketItemsQuery)
            {
                List<GetBasketItemsQueryResponse> response = await _mediator.Send(getBasketItemsQuery);
                return Ok(response);
            }

            [HttpPost]
            public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommand addItemToBasketCommand)
            {
                AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommand);
                return Ok(response);
            }

            [HttpPut]
            public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommand updateQuantityCommand)
            {
                UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommand);
                return Ok(response);
            }

            [HttpDelete("{BasketItemId}")]
            public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommand removeBasketItemCommand)
            {
                RemoveBasketItemCommandResponse response = await _mediator.Send(removeBasketItemCommand);
                return Ok(response);
            }
        }
    }
}
