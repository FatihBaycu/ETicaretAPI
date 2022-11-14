using ETicaretAPI.Application.Features.AppUser.Commands;
using ETicaretAPI.Application.Features.AppUser.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommand loginUserCommandRequest)
        {
            LoginUserCommandResponse? response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommand googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }

        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginCommand facebookLoginCommandRequest)
        {
            FacebookLoginCommandResponse response = await _mediator.Send(facebookLoginCommandRequest);
            return Ok(response);
        }
    }
}
