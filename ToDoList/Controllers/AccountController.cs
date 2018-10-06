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

		public AccountController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
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
			if (!identityResult.Succeeded)
			{
				foreach (var error in identityResult.Errors)
					ModelState.AddModelError(string.Empty, error.Description);

				return View(viewModel);
			}

			return RedirectToAction("SignIn", "Account");
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult SignIn() => View();
	}
}
