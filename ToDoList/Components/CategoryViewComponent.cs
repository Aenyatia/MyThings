using Microsoft.AspNetCore.Mvc;
using ToDoList.Persistence.Extensions;
using ToDoList.Services;

namespace ToDoList.Components
{
	public class CategoryViewComponent : ViewComponent
	{
		private readonly CategoryService _categoryService;

		public CategoryViewComponent(CategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public IViewComponentResult Invoke()
		{
			var userId = HttpContext.User.GetUserId();
			return View("CategoryViewComponent", _categoryService.GetUserCategories(userId));
		}
	}
}
