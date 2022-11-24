using ETicaretAPI.Application.Features.AppUser.Commands.Auths;
using ETicaretAPI.Application.Features.AppUser.Commands.Passwords;
using ETicaretAPI.Application.Features.AppUser.Models.Auths;
using ETicaretAPI.Application.Features.AppUser.Models.Passwords;
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
        
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordCommand command)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(command);
            return Ok(response);
        }

       
    }
}
