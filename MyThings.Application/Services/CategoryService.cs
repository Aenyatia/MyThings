using MyThings.Application.ViewModels.Categories;
using MyThings.Core.Domain;
using MyThings.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace MyThings.Application.Services
{
	public class CategoryService
	{
		private readonly ApplicationDbContext _context;

		public CategoryService(ApplicationDbContext context)
		{
			_context = context;
		}

		public void CreateCategory(string userId, string name)
		{
			var category = Category.Create(userId, name);

			_context.Categories.Add(category);
			_context.SaveChanges();
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
