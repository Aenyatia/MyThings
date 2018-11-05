using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ToDoList.Persistence.Data;
using ToDoList.ViewModels.Categories;

namespace ToDoList.Components
{
	public class CategoriesViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;

		public CategoriesViewComponent(ApplicationDbContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			return View("CategoriesViewComponent", new CategoriesViewModel
			{
				Categories = _context.Categories.Select(c => new CategoryViewModel
				{
					Name = c.Name
				})
			});
		}
	}
}
