using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.DTOs.Orders;
using ETicaretAPI.Application.Features.Orders.Commands;
using ETicaretAPI.Application.Features.Orders.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            CreateOrderCommandResponse response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
