using Microsoft.AspNetCore.Mvc;
using MyThings.Application.Services;

namespace MyThings.Web.Components
{
	public class CategoryViewComponent : ViewComponent
	{
		private readonly CategoryService _categoryService;

		public CategoryViewComponent(CategoryService categoryService)
			=> _categoryService = categoryService;

		public IViewComponentResult Invoke()
			=> View("CategoryViewComponent",
				_categoryService.GetUserCategories());
	}
}
