using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Abstraction.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.DTOs.Facebook;
using ETicaretAPI.Application.Features.AppUser.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ETicaretAPI.Application.Features.AppUser.Commands
{
    public class FacebookLoginCommand : IRequest<FacebookLoginCommandResponse>
    {
        public string AuthToken { get; set; }

    }

    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommand, FacebookLoginCommandResponse>
    {
        private readonly IAuthService _authService;
        public FacebookLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommand request, CancellationToken cancellationToken)
        {
            var token = await _authService.FacebookLoginAsync(request.AuthToken,15);
            return new()
            {
                Token = token
            };
        }
    }


}

