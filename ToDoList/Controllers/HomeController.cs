using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ToDoList.Dtos;
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
			var tasksViewModel = new TasksViewModel
			{
				TodayTasks = _context.Tasks
					.Where(t => t.DueDate.Date == DateTime.Today.Date && t.IsCompleted == false)
					.OrderByDescending(t => t.Priority)
					.Select(t => new TaskViewModel
					{
						Id = t.Id,
						Name = t.Name,
						DueDate = t.DueDate.ToLongDateString(),
						Priority = t.Priority.ToString(),
						Category = t.Category.Name,
					}),

				TomorrowTasks = _context.Tasks
					.Where(t => t.DueDate.Date == DateTime.Today.AddDays(1).Date && t.IsCompleted == false)
					.OrderByDescending(t => t.Priority)
					.Select(t => new TaskViewModel
					{
						Id = t.Id,
						Name = t.Name,
						DueDate = t.DueDate.ToLongDateString(),
						Priority = t.Priority.ToString(),
						Category = t.Category.Name,
					}),

				LaterTasks = _context.Tasks
					.Where(t => t.DueDate.Date > DateTime.Today.AddDays(1).Date && t.IsCompleted == false)
					.OrderBy(t => t.DueDate.Date)
					.ThenByDescending(t => t.Priority)
					.Take(5)
					.Select(t => new TaskViewModel
					{
						Id = t.Id,
						Name = t.Name,
						DueDate = t.DueDate.ToLongDateString(),
						Priority = t.Priority.ToString(),
						Category = t.Category.Name,
					}),

				NotDoneTasks = _context.Tasks
					.Where(t => t.DueDate.Date < DateTime.Today.Date && t.IsCompleted == false)
					.OrderBy(t => t.DueDate.Date)
					.ThenByDescending(t => t.Priority)
					.Select(t => new TaskViewModel
					{
						Id = t.Id,
						Name = t.Name,
						DueDate = t.DueDate.ToLongDateString(),
						Priority = t.Priority.ToString(),
						Category = t.Category.Name,
					}),

				RecentlyCompletedTasks = _context.Tasks
					.Where(t => t.IsCompleted)
					.OrderBy(t => t.CompletedAt.Value)
					.Take(5)
					.Select(t => new TaskViewModel
					{
						Id = t.Id,
						Name = t.Name,
						DueDate = t.DueDate.ToLongDateString(),
						Priority = t.Priority.ToString(),
						Category = t.Category.Name,
					}),

				Categories = _context.Categories
					.Where(c => c.UserId == userId)
					.Select(t => t.Name),

				LaterTasksCount = _context.Tasks.Count(t => t.DueDate.Date > DateTime.Today.AddDays(1).Date && t.IsCompleted == false),

				HistoryTasksCount = _context.Tasks.Count(t => t.IsCompleted)
			};

			return View(tasksViewModel);
		}
	}
}
