using ETicaretAPI.Application.Features.AppUser.Commands;
using ETicaretAPI.Application.Features.AppUser.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            CreateUserCommandModel model = await _mediator.Send(command);
            return Ok(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommand loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommand command)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginCommand command)
        {
            FacebookLoginCommandResponse response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
