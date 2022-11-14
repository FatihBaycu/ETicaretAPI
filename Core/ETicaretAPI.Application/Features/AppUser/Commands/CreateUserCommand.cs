using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Features.AppUser.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Application.Features.AppUser.Commands
{
    public class CreateUserCommand : IRequest<CreateUserCommandModel>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandModel>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

          CreateUserResponse response=  await _userService.CreateAsync(new()
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                Password = request.Password,
                RePassword = request.RePassword,
                Username = request.Username,
            });

            return new()
            {
                Message=response.Message,
                Succeeded=response.Succeeded
            };
 
        }
    }
}
