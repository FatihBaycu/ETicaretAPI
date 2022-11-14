
using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Abstraction.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.DTOs.Facebook;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Exceptions.ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {

        readonly HttpClient _httpClient;
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHelper;
        readonly IConfiguration _configuration;
        readonly SignInManager<AppUser> _signInManager;


        public AuthService(IHttpClientFactory httpClientFactory, UserManager<AppUser> userManager, ITokenHandler tokenHelper, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenHelper = tokenHelper;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _signInManager = signInManager;
        }

        async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, string firstName, string lastName, UserLoginInfo info, int accessTokenLifeTime)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        Name = firstName,
                        Surname = lastName,
                      };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

                Token token = _tokenHelper.CreateAccessToken(accessTokenLifeTime);
                return token;
            }
            throw new Exception("Invalid external authentication.");
        }


        public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync(
                $"https://graph.facebook.com/oauth/access_token?client_id=" + _configuration["FacebookLogin:ClientId"] +
                "&client_secret=" + _configuration["FacebookLogin:SecretKey"] + "&grant_type=client_credentials");

            FacebookAccessTokenResponse? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);
            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

            FacebookUserAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);
            if (validation?.Data.IsValid != null)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,first_name,last_name,name&access_token={authToken}");
                FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, userInfo.FirstName, userInfo.LastName, info, accessTokenLifeTime);
                // return await CreateUserExternalAsync(user, userInfo!.Email, userInfo.FirstName, userInfo.LastName, info, 15);
            }
            throw new Exception("Invalid external auth.");

        }
        public async Task<Token> GoogleLoginAsync(string idToken, string firstName, string lastName, int accessTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["GoogleLogin:ClientId"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, payload.Email, payload.Name, firstName, lastName, info, accessTokenLifeTime);
        }



        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded) //Authentication başarılı!
            {
                Token token = _tokenHelper.CreateAccessToken(accessTokenLifeTime);
                return token;
            }
            throw new AuthenticationErrorException();
        }





    }
}
