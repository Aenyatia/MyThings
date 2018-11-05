using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ToDoList.Dtos;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Extensions;
using ToDoList.ViewModels;
using ToDoList.ViewModels.Tasks;

namespace ToDoList.Controllers
{
	public class TasksController : Controller
	{
		private readonly ApplicationDbContext _context;

		public TasksController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var task = _context.Tasks.SingleOrDefault(t => t.Id == id);
			if (task == null)
				return NotFound();

			return Ok(task);
		}

		[HttpGet]
		public IActionResult Edit()
		{
			return View("EditTask");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int taskId, [FromBody]EditTaskViewModel viewModel)
		{
			var userId = User.GetUserId();
			var task = _context.Tasks.SingleOrDefault(t => t.Id == taskId && t.UserId == userId);

			if (task == null)
				return BadRequest();

			task.Edit(viewModel.Name, viewModel.Priority, viewModel.DueDate, viewModel.Category);
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public IActionResult Complete(int taskId)
		{
			var userId = User.GetUserId();
			var task = _context.Tasks.SingleOrDefault(t => t.Id == taskId && t.UserId == userId);

			if (task == null)
				return BadRequest();

			task.Deactivate();
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public IActionResult Active(int taskId)
		{
			var userId = User.GetUserId();
			var task = _context.Tasks.SingleOrDefault(t => t.Id == taskId && t.UserId == userId);

			if (task == null)
				return BadRequest();

			task.Activate();
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

		public IActionResult Delete()
		{
			throw new System.NotImplementedException();
		}

		public IActionResult TodayTasks()
		{
			var userId = User.GetUserId();
			var viewModel = new TodayTasksViewModel
			{
				TodayTasks = _context.Tasks.Where(t => t.UserId == userId &&
													   t.DueDate.Date == DateTime.Today.Date &&
													   t.IsCompleted == false)
					.OrderByDescending(t => t.Priority)
					.Select(t => new TaskViewModel
					{
						Id = t.Id,
						Name = t.Name,
						DueDate = t.DueDate.ToLongDateString(),
						Priority = t.Priority.ToString(),
						Category = t.Category.Name,
					}),

				TasksCategoriesViewModel = new TasksCategoriesViewModel
				{
					Categories = _context.Categories
						.Where(c => c.UserId == userId)
						.Select(t => t.Name),

					TaskCounters = new TaskNumbersDto
					{
						TodayTasks = _context.Tasks.Count(t => t.DueDate.Date == DateTime.Today.Date && t.IsCompleted == false),
						TomorrowTasks = _context.Tasks.Count(t => t.DueDate.Date == DateTime.Today.AddDays(1).Date && t.IsCompleted == false),
						LaterTasks = _context.Tasks.Count(t => t.DueDate.Date > DateTime.Today.AddDays(1).Date && t.IsCompleted == false),
						NotDoneTasks = _context.Tasks.Count(t => t.DueDate.Date < DateTime.Today.Date && t.IsCompleted == false),
						HistoryTasks = _context.Tasks.Count(t => t.IsCompleted)
					}
				}
			};

			return View("TodayTasks", viewModel);
		}
	}
}
