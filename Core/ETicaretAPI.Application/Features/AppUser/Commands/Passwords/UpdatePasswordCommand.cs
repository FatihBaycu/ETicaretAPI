using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Features.AppUser.Models.Passwords;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.AppUser.Commands.Passwords
{
    public class UpdatePasswordCommand:IRequest<UpdatePasswordCommandResponse>
    {
        public string UserId { get; set; }
        public string ResetToken { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UpdatePasswordCommandResponse>
    {
        readonly IUserService _userService;
        public UpdatePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.Password.Equals(request.PasswordConfirm))
                throw new PasswordChangeFieldException("Please confirm the password");

            await _userService.UpdatePasswordAsync(request.UserId,request.ResetToken,request.Password);
            return new();
        }
    }
}
