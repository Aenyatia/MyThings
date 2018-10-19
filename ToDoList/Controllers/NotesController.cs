using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ToDoList.Models;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Extensions;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
	[Authorize]
	public class NotesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public NotesController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Mine()
		{
			var userId = User.GetUserId();

			return View();
		}

		[HttpGet]
		public IActionResult AddNote() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddNote(TaskViewModel viewModel)
		{
			var userId = User.GetUserId();
			var note = new ToDo
			{
				Author = userId,
			
			};

			_context.SaveChanges();

			return RedirectToAction("Mine", "Notes");
		}
	}
}
