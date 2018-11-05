using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Extensions;
using ToDoList.ViewModels.Categories;

namespace ToDoList.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CategoriesController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult GetUserCategories()
		{
			var userId = User.GetUserId();
			var categories = _context.Categories
				.Where(c => c.UserId == userId)
				.Select(c => new CategoryViewModel { Name = c.Name })
				.ToList();

			return View(categories);
		}

		public IActionResult Remove(int id)
		{

		}
	}
}