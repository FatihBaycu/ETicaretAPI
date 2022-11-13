using ETicaretAPI.Application.Abstraction.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Exceptions.ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Features.AppUser.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.AppUser.Commands
{
    public class LoginUserCommand : IRequest<LoginUserCommandResponse>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
    public class LoginUserSuccessCommand : LoginUserCommandResponse
    {
        public Token Token { get; set; }
    }
    public class LoginUserErrorCommand : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }


    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;


        public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user == null)
                throw new NotFoundUserException("Kullanıcı veya şifre hatalı...");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded) 
            {
                Token token = _tokenHandler.CreateAccessToken(5);
                return new LoginUserSuccessCommand()
                {
                    Token = token
                };
            }

            //return new LoginUserErrorCommandResponse()
            //{
            //    Message = "Kullanıcı adı veya şifre hatalı..."
            //};
            throw new AuthenticationErrorException();
        }
    }
}
