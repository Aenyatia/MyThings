using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyThings.Application.Services;
using MyThings.Infrastructure.Extensions;
using MyThings.Web.Commands;

namespace MyThings.Web.Controllers
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
		public IActionResult CreateCategory() => View();

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult CreateCategory(CreateCategoryCommand command)
		{
			if (!ModelState.IsValid)
				return View(command);

			_categoryService.CreateCategory(User.GetUserId(), command.Name);

			return RedirectToAction("", "");
		}

		[HttpDelete]
		public IActionResult DeleteCategory(int categoryId)
		{
			_categoryService.DeleteCategory(User.GetUserId(), categoryId);

			return RedirectToAction("", "");
		}
	}
}
