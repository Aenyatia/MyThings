using Microsoft.AspNetCore.Mvc;

namespace MyThings.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("Summary", "Tasks");

			return View();
		}
	}
}
