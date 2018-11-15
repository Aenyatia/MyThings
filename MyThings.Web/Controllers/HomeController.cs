using Microsoft.AspNetCore.Mvc;

namespace MyThings.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index() => View();
	}
}
