using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyThings.Application.Services;
using MyThings.Web.Commands;
using MyThings.Web.Filters;

namespace MyThings.Web.Controllers
{
	[Authorize]
	[TypeFilter(typeof(UserContextFilter))]
	public class CategoriesController : Controller
	{
		private readonly CategoryService _categoryService;

		public CategoriesController(CategoryService categoryService)
			=> _categoryService = categoryService;

		[HttpGet]
		public IActionResult CreateCategory() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateCategory(CreateCategoryCommand command)
		{
			if (!ModelState.IsValid)
				return View(command);

			_categoryService.CreateCategory(command.Name);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpGet]
		public IActionResult ManageCategories()
			=> View(_categoryService.GetUserCategories());

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteCategory(int categoryId)
		{
			_categoryService.DeleteCategory(categoryId);

			return RedirectToAction("ManageCategories", "Categories");
		}
	}
}
