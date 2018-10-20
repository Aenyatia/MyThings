using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ToDoList.Dtos;
using ToDoList.Extensions;
using ToDoList.Models;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Extensions;

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
			if (dto.Name.IsEmpty())
				return BadRequest();

			var userId = User.GetUserId();
			var category = Category.Create(userId, dto.Name);

			_context.Categories.Add(category);
			_context.SaveChanges();

			return Created(string.Empty, null);
		}

		[HttpPost("{id}")]
		public IActionResult Delete(int id)
		{
			var userId = User.GetUserId();
			var category = _context.Categories.SingleOrDefault(c => c.UserId == userId && c.Id == id);
			if (category == null)
				return BadRequest();

			_context.Categories.Remove(category);
			_context.SaveChanges();

			return NoContent();
		}
	}
}
