using EventAPI.UI.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user is null)
            {
                ModelState.AddModelError(String.Empty, "User not found");
                return View(loginModel);
            }

            var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password,
                true, false);

            if (result.Succeeded)
            {
                return Redirect(loginModel.ReturnUrl);
            }

            ModelState.AddModelError(String.Empty, "Login or password is incorrect");
            return View(loginModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }

            var user = new IdentityUser
            {
                UserName = registerModel.UserName
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, true);
                return Redirect(registerModel.ReturnUrl);
            }

            ModelState.AddModelError(String.Empty, "Error occured");
            return View(registerModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
