using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Extensions;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _context;

		public HomeController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var userId = User.GetUserId();

			var tasks = new TasksViewModel
			{
				ActiveTasks = _context.Tasks
					.Where(t => t.DueDate.Date >= DateTime.Today.Date && t.IsCompleted == false)
					.OrderBy(t => t.DueDate.Date)
					.ThenByDescending(t => t.Priority)
					.ToList(),

				CompletedTasks = _context.Tasks
					.Where(t => t.IsCompleted)
					.OrderBy(t => t.DueDate)
					.ToList(),

				Categories = _context.Categories
					.Where(c => c.UserId == userId)
					.ToList()
			};

			return View(tasks);
		}
	}
}
