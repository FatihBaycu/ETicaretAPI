using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Application.Exceptions.ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Features.AppUser.Models;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {

            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };
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
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user,DateTime accessTokenDate,int addOnAccessToken)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddMinutes(addOnAccessToken);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }
    }
}
