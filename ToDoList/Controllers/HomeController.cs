using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Extensions;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _context;

		public HomeController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var userId = User.GetUserId();

			var tasks = new TasksViewModel
			{
				Tasks = _context.Tasks.ToList(),
				Categories = _context.Categories.Where(c => c.UserId == userId)
			};

			return View(tasks);
		}
	}
}
