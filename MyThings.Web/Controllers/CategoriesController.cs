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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateCategory(CreateCategoryCommand command)
		{
			if (!ModelState.IsValid)
				return RedirectToAction("Index", "Home");

			_categoryService.CreateCategory(User.GetUserId(), command.Name);

			return Created(string.Empty, null);
		}
	}
}
