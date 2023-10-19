using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JuntoSegurosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(Models.RegisterModel model)
        {
            var (Succeeded, Errors, Token) = await _accountService.RegisterAsync(model);
            if (Succeeded)
            {
                return Ok(new { Token });
            }
            return BadRequest(Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(Models.LoginModel model)
        {
            var (Succeeded, Errors, Token) = await _accountService.LoginAsync(model);
            if (Succeeded)
            {
                return Ok(new { Token });
            }
            return BadRequest(Errors);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePasswordAsync(Models.ChangePasswordModel model)
        {
            var (Succeeded, Errors) = await _accountService.ChangePasswordAsync(User, model);
            if (Succeeded)
            {
                return Ok();
            }
            return BadRequest(Errors);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(string email)
        {
            var (Succeeded, Errors) = await _accountService.ResetPasswordAsync(email);
            if (Succeeded)
            {
                return Ok();
            }
            return BadRequest(Errors);
        }
    }

}
