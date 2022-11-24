using ETicaretAPI.Application.Features.AppUser.Commands.Auths;
using ETicaretAPI.Application.Features.AppUser.Commands.Passwords;
using ETicaretAPI.Application.Features.AppUser.Models.Auths;
using ETicaretAPI.Application.Features.AppUser.Models.Passwords;
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

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommand command)
        {
            RefreshTokenLoginCommandResponse? response = await _mediator.Send(command);
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
        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset(PasswordResetCommand command)
        {
            PasswordResetCommandResponse response= await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommand command )
        {
            VerifyResetTokenCommandResponse response= await _mediator.Send(command);
            return Ok(response);
        }
    }
}
