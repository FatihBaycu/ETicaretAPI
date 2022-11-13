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
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Username,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
            }, request.Password);

            CreateUserCommandModel response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message = $"{error.Code} -  {error.Description}\n";
                }

            }

            return response;
            //throw new UserCreateFailedException();

        }
    }
}
