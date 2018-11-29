using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyThings.Web.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("Summary", "Tasks");

			return View();
		}
	}
}
