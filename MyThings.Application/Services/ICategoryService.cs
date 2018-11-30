using MyThings.Application.Dtos;
using System.Collections.Generic;

namespace MyThings.Application.Services
{
	public interface ICategoryService
	{
		IEnumerable<CategoryDto> GetUserCategories();
		void CreateCategory(string name);
		void DeleteCategory(int categoryId);
	}
}
