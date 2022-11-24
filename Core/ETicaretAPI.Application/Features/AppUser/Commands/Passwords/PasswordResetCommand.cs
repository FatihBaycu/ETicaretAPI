using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Features.AppUser.Models.Passwords;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.AppUser.Commands.Passwords
{
    public class PasswordResetCommand : IRequest<PasswordResetCommandResponse>
    {
        public string Email { get; set; }
    }
    public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand, PasswordResetCommandResponse>
    {
        readonly IAuthService _authService;

        public PasswordResetCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
        {
            await _authService.PasswordResetAsync(request.Email);
            return new();
        }
    }
}
