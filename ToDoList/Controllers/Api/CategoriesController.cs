using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Extensions;
using ToDoList.ViewModels;

namespace ToDoList.Controllers.Api
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class CategoriesController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public CategoriesController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public IActionResult Post([FromBody]CategoryDto dto)
		{
			var userId = User.GetUserId();
			var category = Category.Create(userId, dto.Name);
			_context.Categories.Add(category);
			_context.SaveChanges();

			return Created(string.Empty, null);
		}
	}
}
