using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyThings.Application.Services;
using MyThings.Application.ViewModels.Categories;
using MyThings.Infrastructure.Extensions;
using ToDoList.Commands;

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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateCategory(CreateCategoryCommand command)
		{
			if (!ModelState.IsValid)
				return RedirectToAction("Index", "Home");

			_categoryService.CreateCategory(User.GetUserId(), command.Name);

			return Created(string.Empty, null);
		}

		[HttpGet]
		public IActionResult ManageCategories()
		{
			var userId = User.GetUserId();
			return View(_categoryService.GetUserCategories(userId));
		}
	}
}
