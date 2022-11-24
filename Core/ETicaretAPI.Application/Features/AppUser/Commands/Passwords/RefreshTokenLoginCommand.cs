using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Features.AppUser.Models.Passwords;
using MediatR;

namespace ETicaretAPI.Application.Features.AppUser.Commands.Passwords
{
    public class RefreshTokenLoginCommand : IRequest<RefreshTokenLoginCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommand, RefreshTokenLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommand request, CancellationToken cancellationToken)
        {
            Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return new()
            {
                Token = token
            };
        }
    }
}
