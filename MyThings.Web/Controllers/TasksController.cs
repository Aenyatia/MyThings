using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyThings.Application.Dtos;
using MyThings.Application.Services;
using MyThings.Application.Specifications;
using MyThings.Infrastructure.Extensions;
using MyThings.Web.Commands;
using MyThings.Web.ViewModels;

namespace MyThings.Web.Controllers
{
	[Authorize]
	public class TasksController : Controller
	{
		private readonly TaskService _taskService;
		private readonly CategoryService _categoryService;

		public TasksController(TaskService taskService, CategoryService categoryService)
		{
			_taskService = taskService;
			_categoryService = categoryService;
		}

		[HttpGet("tasks/today")]
		public IActionResult GetTodayTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new TodayTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet("tasks/tomorrow")]
		public IActionResult GetTomorrowTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new TomorrowTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet("tasks/later")]
		public IActionResult GetLaterTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new LaterTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet("tasks/undone")]
		public IActionResult GetUndoneTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new NotDoneTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet("tasks/completed")]
		public IActionResult GetCompletedTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new CompletedTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet("tasks/category/{categoryId}")]
		public IActionResult GetTasksByCategory(int categoryId)
		{
			var tasks = _taskService.GetTasksByCategory(User.GetUserId(), categoryId);

			return View("Tasks", tasks);
		}

		[HttpGet("tasks/details/{taskId}")]
		public IActionResult GetTaskDetails(int taskId)
		{
			var task = _taskService.GetTaskById(User.GetUserId(), taskId);

			return View("Details", task);
		}

		[HttpGet("tasks/summary")]
		public IActionResult Summary()
		{
			var userId = User.GetUserId();
			var viewModel = new SummaryViewModel
			{
				TodayTasks = _taskService.GetTasks(new TodayTasksSpecification(userId), 5),
				TomorrowTasks = _taskService.GetTasks(new TomorrowTasksSpecification(userId), 5),
				LaterTasks = _taskService.GetTasks(new LaterTasksSpecification(userId), 5),
				RecentlyCompletedTasks = _taskService.GetTasks(new CompletedTasksSpecification(userId), 5)
			};

			return View(viewModel);
		}

		[HttpGet]
		public IActionResult CreateTask() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateTask(CreateTaskCommand command)
		{
			if (!ModelState.IsValid)
				return View(command);

			_taskService.CreateTask(User.GetUserId(), command.Name);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpGet]
		public IActionResult EditTask(int taskId)
		{
			var task = _taskService.GetTaskById(User.GetUserId(), taskId);

			if (task == null)
				return NotFound();

			var command = new EditTaskCommand
			{
				Id = task.Id,
				Name = task.Name,
				DueDate = task.DueDate,
				PriorityId = task.Priority,
				CategoryId = task.CategoryId,
				Categories = _categoryService.GetUserCategories(User.GetUserId())
			};

			return View(command);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditTask(EditTaskCommand command)
		{
			if (!ModelState.IsValid)
			{
				command.Categories = _categoryService.GetUserCategories(User.GetUserId());
				return View(command);
			}

			var taskDto = new TaskDto
			{
				Id = command.Id,
				Name = command.Name,
				DueDate = command.DueDate,
				Priority = command.PriorityId,
				CategoryId = command.GetCategoryId()
			};

			_taskService.UpdateTask(User.GetUserId(), taskDto);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteTask(int taskId)
		{
			_taskService.DeleteTask(User.GetUserId(), taskId);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ActivateTask(int taskId)
		{
			_taskService.Activate(User.GetUserId(), taskId);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeactivateTask(int taskId)
		{
			_taskService.Deactivate(User.GetUserId(), taskId);

			return RedirectToAction("Summary", "Tasks");
		}
	}
}
