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
            var result = await _accountService.RegisterAsync(model);
            if (result.Succeeded)
            {
                return Ok(new { Token = result.Token });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(Models.LoginModel model)
        {
            var result = await _accountService.LoginAsync(model);
            if (result.Succeeded)
            {
                return Ok(new { Token = result.Token });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePasswordAsync(Models.ChangePasswordModel model)
        {
            var result = await _accountService.ChangePasswordAsync(User, model);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(string email)
        {
            var result = await _accountService.ResetPasswordAsync(email);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }
    }

}
