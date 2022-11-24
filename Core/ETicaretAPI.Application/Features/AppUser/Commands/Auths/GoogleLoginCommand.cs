using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Features.AppUser.Models.Auths;
using MediatR;

namespace ETicaretAPI.Application.Features.AppUser.Commands.Auths
{
    public class GoogleLoginCommand : IRequest<GoogleLoginCommandResponse>
    {
        public string Id { get; set; }
        public string IdToken { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Provider { get; set; }
    }
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, GoogleLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public GoogleLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            var token = await _authService.GoogleLoginAsync(request.IdToken, request.FirstName, request.LastName, 15);
            return new()
            {
                Token = token
            };

        }
    }
}