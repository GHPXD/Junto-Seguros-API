using JuntoSegurosAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace JuntoSegurosAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public object JwtRegisteredClaimNames { get; private set; }

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<(bool Succeeded, string[] Errors, string Token)> RegisterAsync(RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return (true, new string[] { }, GenerateJwtToken(model.Email, user));
            }

            return (false, result.Errors.Select(x => x.Description).ToArray(), null);
        }

        public async Task<(bool Succeeded, string[] Errors, string Token)> LoginAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return (true, new string[] { }, GenerateJwtToken(model.Email, appUser));
            }

            return (false, new string[] { "Invalid login attempt." }, null);
        }

        public async Task<(bool Succeeded, string[] Errors)> ChangePasswordAsync(ClaimsPrincipal userClaimsPrincipal, ChangePasswordModel model)
        {
            var user = await _userManager.GetUserAsync(userClaimsPrincipal);
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return (true, new string[] { });
            }

            return (false, result.Errors.Select(x => x.Description).ToArray());
        }

        private string GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
         {
             new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, email),
             new Claim(System.IdentityModel.Tokens.Jwt.Jti, Guid.NewGuid().ToString()),
             new Claim(ClaimTypes.NameIdentifier, user.Id)
         };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenInformation:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["TokenInformation:ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["TokenInformation:Issuer"],
                _configuration["TokenInformation:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Task ResetPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
