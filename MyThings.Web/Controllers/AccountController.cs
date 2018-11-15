using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyThings.Infrastructure.Identity;
using MyThings.Web.Commands;
using System.Threading.Tasks;

namespace MyThings.Web.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult SignUp() => View();

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignUp(SignUpCommand command)
		{
			if (!ModelState.IsValid)
				return View(command);

			var user = new ApplicationUser
			{
				UserName = command.Email,
				Email = command.Email
			};

			var identityResult = await _userManager.CreateAsync(user, command.Password);

			if (identityResult.Succeeded)
				return RedirectToAction("LogIn", "Account");

			foreach (var error in identityResult.Errors)
				ModelState.AddModelError(string.Empty, error.Description);

			return View(command);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult LogIn(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View("SignIn");
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogIn(SignInCommand command, string returnUrl)
		{
			if (!ModelState.IsValid)
				return View("SignIn", command);

			var user = await _userManager.FindByEmailAsync(command.Email);
			if (user != null)
			{
				await _signInManager.SignOutAsync();

				var result = await _signInManager.PasswordSignInAsync(user, command.Password, command.RememberMe, false);
				if (result.Succeeded)
					return Redirect(returnUrl ?? "/");
			}

			ModelState.AddModelError(string.Empty, "Invalid email or password.");
			return View("SignIn", command);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
