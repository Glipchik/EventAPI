using EventAPI.UI.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public RegisterController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new IdentityUser
            {
                UserName = registerModel.UserName
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, true);
                return Ok();
            }

            ModelState.AddModelError(String.Empty, "Error occured");
            return BadRequest();
        }
    }
}
