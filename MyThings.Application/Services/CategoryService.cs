using AutoMapper;
using MyThings.Application.Dtos;
using MyThings.Core.Domain;
using MyThings.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyThings.Application.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ApplicationDbContext _context;
		private readonly IUserContext _userContext;
		private readonly IMapper _mapper;

		public CategoryService(ApplicationDbContext context, IUserContext userContext, IMapper mapper)
		{
			_context = context;
			_userContext = userContext;
			_mapper = mapper;
		}

		public IEnumerable<CategoryDto> GetUserCategories()
		{
			var categories = _context.Categories
				.Where(c => c.UserId == _userContext.UserId)
				.ToList();

			return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);
		}

		public void CreateCategory(string name)
		{
			var category = Category.Create(_userContext.UserId, name);

			_context.Categories.Add(category);
			_context.SaveChanges();
		}

		public void DeleteCategory(int categoryId)
		{
			var category = _context.Categories
				.SingleOrDefault(c => c.Id == categoryId && c.UserId == _userContext.UserId);

			if (category == null)
				throw new ArgumentException(nameof(categoryId));

			_context.Categories.Remove(category);
			_context.SaveChanges();
		}
	}
}
