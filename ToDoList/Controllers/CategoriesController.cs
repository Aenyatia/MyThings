using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Extensions;
using ToDoList.ViewModels.Categories;

namespace ToDoList.Controllers
{
	[Authorize]
	public class CategoriesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CategoriesController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult ManageCategories()
		{
			var userId = User.GetUserId();
			var categories = _context.Categories
				.Where(c => c.UserId == userId)
				.Select(c => new CategoryViewModel { Name = c.Name })
				.ToList();

			return View("ManageCategories", categories);
		}

		public IActionResult Remove(int id)
		{
			var userId = User.GetUserId();
			var category = _context.Categories.SingleOrDefault(c => c.Id == id && c.UserId == userId);
			if (category == null)
				throw new ArgumentException();

			_context.Categories.Remove(category);
			_context.SaveChanges();

			return NoContent();
		}
	}
}
