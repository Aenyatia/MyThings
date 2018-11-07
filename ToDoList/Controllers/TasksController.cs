using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Extensions;
using ToDoList.Services;
using ToDoList.ViewModels;
using ToDoList.ViewModels.Tasks;

namespace ToDoList.Controllers
{
	public class TasksController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly TaskService _taskService;

		public TasksController(ApplicationDbContext context, TaskService taskService)
		{
			_context = context;
			_taskService = taskService;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var task = _context.Tasks.SingleOrDefault(t => t.Id == id);
			if (task == null)
				return NotFound();

			return Ok(task);
		}

		[HttpGet("{taskId}")]
		public IActionResult Edit(int taskId)
		{
			var userId = User.GetUserId();
			var task = _context.Tasks.SingleOrDefault(t => t.Id == taskId && t.UserId == userId);
			if (task == null)
				throw new ArgumentException();

			var viewModel = new EditTaskViewModel
			{
				Name = task.Name,
				DueDate = task.DueDate,
				Priority = task.Priority,
				Category = task.Category
			};

			return View("EditTask", viewModel);
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

		[HttpGet]
		public IActionResult TodayTasks()
		{
			var userId = User.GetUserId();
			var viewModel = new TasksViewModel
			{
				Tasks = _taskService.GetTodayTasks(userId),
				TaskOption = "today"
			};

			return View("Tasks", viewModel);
		}

		[HttpGet]
		public IActionResult TomorrowTasks()
		{
			var userId = User.GetUserId();
			var viewModel = new TasksViewModel
			{
				Tasks = _taskService.GetTomorrowTasks(userId),
				TaskOption = "tomorrow"
			};

			return View("Tasks", viewModel);
		}

		[HttpGet]
		public IActionResult LaterTasks()
		{
			var userId = User.GetUserId();
			var viewModel = new TasksViewModel
			{
				Tasks = _taskService.GetLaterTasks(userId),
				TaskOption = "later"
			};

			return View("Tasks", viewModel);
		}

		[HttpGet]
		public IActionResult NotDoneTasks()
		{
			var userId = User.GetUserId();
			var viewModel = new TasksViewModel
			{
				Tasks = _taskService.GetNotDoneTasks(userId),
				TaskOption = "notdone"
			};

			return View("Tasks", viewModel);
		}

		[HttpGet]
		public IActionResult CompletedTasks()
		{
			var userId = User.GetUserId();
			var viewModel = new TasksViewModel
			{
				Tasks = _taskService.GetCompletedTasks(userId),
				TaskOption = "completed"
			};

			return View("Tasks", viewModel);
		}

		[HttpGet("{categoryId}")]
		public IActionResult TaskWithCategory(int categoryId)
		{
			var userId = User.GetUserId();
			var viewModel = new TasksViewModel
			{
				Tasks = _context.Tasks
					.Where(t => t.Category.Id == categoryId &&
								t.UserId == userId)
					.OrderByDescending(t => t.Priority)
					.Select(t => new TaskViewModel
					{
						Id = t.Id,
						Name = t.Name,
						DueDate = t.DueDate.ToLongDateString(),
						Priority = t.Priority.ToString(),
						Category = t.Category.Name
					})
					.ToList(),

				TaskOption = "category"
			};

			return View("Tasks", viewModel);
		}
	}
}
