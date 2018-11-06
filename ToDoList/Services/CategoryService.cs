using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;
using ToDoList.Persistence.Data;
using ToDoList.ViewModels.Categories;

namespace ToDoList.Services
{
	public class CategoryService
	{
		private readonly ApplicationDbContext _context;

		public CategoryService(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<CategoryViewModel> GetUserCategories(string userId)
		{
			var categories = _context.Categories.Where(c => c.UserId == userId);

			return CreateCategoryViewModel(categories);
		}

		private static IEnumerable<CategoryViewModel> CreateCategoryViewModel(IQueryable<Category> categories)
		{
			return categories
				.Select(c => new CategoryViewModel
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToList();
		}
	}
}
