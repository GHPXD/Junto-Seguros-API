using JuntoSegurosAPI.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JuntoSegurosAPI.Controllers
{
    public interface IAccountService
    {
        Task<(bool Succeeded, string[] Errors, string Token)> RegisterAsync(RegisterModel model);
        Task<(bool Succeeded, string[] Errors, string Token)> LoginAsync(LoginModel model);
        Task<(bool Succeeded, string[] Errors)> ChangePasswordAsync(ClaimsPrincipal userClaimsPrincipal, ChangePasswordModel model);
        Task<(bool Succeeded, string[] Errors)> ResetPasswordAsync(string email);
    }

}