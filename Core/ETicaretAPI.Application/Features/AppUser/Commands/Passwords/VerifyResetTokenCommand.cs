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
    public class VerifyResetTokenCommand : IRequest<VerifyResetTokenCommandResponse>
    {
        public string UserId { get; set; }
        public string ResetToken { get; set; }
    }
    public class VerifyResetTokenCommandHandler : IRequestHandler<VerifyResetTokenCommand, VerifyResetTokenCommandResponse>
    {
        readonly IAuthService _authService;

        public VerifyResetTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<VerifyResetTokenCommandResponse> Handle(VerifyResetTokenCommand request, CancellationToken cancellationToken)
        {
            bool state = await _authService.VerifyResetTokenAsycn(request.UserId, request.ResetToken);
            return new()
            {
                State = state,
            };
        }
    }
}
