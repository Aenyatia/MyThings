using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Persistence.Extensions;
using ToDoList.Services;

namespace ToDoList.Controllers
{
	[Authorize]
	public class CategoriesController : Controller
	{
		private readonly CategoryService _categoryService;

		public CategoriesController(CategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public IActionResult ManageCategories()
		{
			var userId = User.GetUserId();
			return View(_categoryService.GetUserCategories(userId));
		}
	}
}
