using AutoMapper;
using MyThings.Application.Dtos;
using MyThings.Core.Domain;
using MyThings.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyThings.Application.Services
{
	public class CategoryService
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CategoryService(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public IEnumerable<CategoryDto> GetUserCategories(string userId)
		{
			var categories = _context.Categories.Where(c => c.UserId == userId).ToList();

			return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);
		}

		public void CreateCategory(string userId, string name)
		{
			var category = Category.Create(userId, name);

			_context.Categories.Add(category);
			_context.SaveChanges();
		}

		public void DeleteCategory(string userId, int categoryId)
		{
			var category = _context.Categories.SingleOrDefault(c => c.Id == categoryId && c.UserId == userId);

			if (category == null)
				throw new ArgumentException(nameof(categoryId));

			_context.Categories.Remove(category);
			_context.SaveChanges();
		}
	}
}
