using EventAPI.UI.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user is null)
            {
                ModelState.AddModelError(String.Empty, "User not found");
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password,
                true, false);

            if (result.Succeeded)
            {
                return Ok();
            }

            ModelState.AddModelError(String.Empty, "Login or password is incorrect");
            return BadRequest();
        }
    }
}
