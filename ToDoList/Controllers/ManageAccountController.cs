using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.ViewModels.Account;

namespace ToDoList.Controllers
{
	[Authorize]
	public class ManageAccountController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			var viewModel = new EditCredentialsViewModel
			{
				OldEmail = "geralt@gmail.com"
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ChangeEmail()
		{
			return NoContent();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ChangePassword()
		{
			return NoContent();
		}
	}
}
