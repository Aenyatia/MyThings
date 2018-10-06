using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Persistence.Identity;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
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
		public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			var user = new ApplicationUser
			{
				UserName = viewModel.Email,
				Email = viewModel.Email
			};

			var identityResult = await _userManager.CreateAsync(user, viewModel.Password);

			if (identityResult.Succeeded)
				return RedirectToAction("SignIn", "Account");

			foreach (var error in identityResult.Errors)
				ModelState.AddModelError(string.Empty, error.Description);

			return View(viewModel);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult SignIn() => View();

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn(SignInViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			var user = await _userManager.FindByEmailAsync(viewModel.Email);
			if (user != null)
			{
				await _signInManager.SignOutAsync();

				var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);
				if (result.Succeeded)
					return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError(string.Empty, "Invalid email or password.");
			return View(viewModel);
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
