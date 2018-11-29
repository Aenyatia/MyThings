using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyThings.Application.Dtos;
using MyThings.Application.Services;
using MyThings.Application.Specifications;
using MyThings.Web.Commands;
using MyThings.Web.Filters;
using MyThings.Web.ViewModels;
using System.Collections.Generic;

namespace MyThings.Web.Controllers
{
	[Authorize]
	[TypeFilter(typeof(UserContextFilter))]
	public class TasksController : Controller
	{
		private readonly TaskService _taskService;
		private readonly CategoryService _categoryService;
		private readonly IUserContext _userContext;

		public TasksController(TaskService taskService, CategoryService categoryService, IUserContext userContext)
		{
			_taskService = taskService;
			_categoryService = categoryService;
			_userContext = userContext;
		}

		[HttpGet("tasks/today")]
		public IActionResult GetTodayTasks()
		{
			var tasks = _taskService.GetTasks(new TodayTasksSpecification(_userContext.UserId));

			return View("Tasks", CreateTasksViewModel("today", tasks));
		}

		[HttpGet("tasks/tomorrow")]
		public IActionResult GetTomorrowTasks()
		{
			var tasks = _taskService.GetTasks(new TomorrowTasksSpecification(_userContext.UserId));

			return View("Tasks", CreateTasksViewModel("tomorrow", tasks));
		}

		[HttpGet("tasks/later")]
		public IActionResult GetLaterTasks()
		{
			var tasks = _taskService.GetTasks(new LaterTasksSpecification(_userContext.UserId));

			return View("Tasks", CreateTasksViewModel("later", tasks));
		}

		[HttpGet("tasks/undone")]
		public IActionResult GetUndoneTasks()
		{
			var tasks = _taskService.GetTasks(new NotDoneTasksSpecification(_userContext.UserId));

			return View("Tasks", CreateTasksViewModel("not done", tasks));
		}

		[HttpGet("tasks/completed")]
		public IActionResult GetCompletedTasks()
		{
			var tasks = _taskService.GetTasks(new CompletedTasksSpecification(_userContext.UserId));

			return View("Tasks", CreateTasksViewModel("completed", tasks));
		}

		[HttpGet("tasks/category/{categoryId}")]
		public IActionResult GetTasksByCategory(int categoryId)
		{
			var tasks = _taskService.GetTasksByCategory(
				new TasksWithCategory(_userContext.UserId, categoryId));

			return View("Tasks", CreateTasksViewModel("category", tasks));
		}

		[HttpGet("tasks/details/{taskId}")]
		public IActionResult GetTaskDetails(int taskId)
		{
			var task = _taskService.GetTaskById(taskId);

			if (task == null)
				return NotFound();

			return View("Details", task);
		}

		[HttpGet("tasks/summary")]
		public IActionResult Summary()
		{
			var viewModel = new SummaryViewModel
			{
				TodayTasks = _taskService.GetTasks(
					new TodayTasksSpecification(_userContext.UserId), 3),
				TomorrowTasks = _taskService.GetTasks(
					new TomorrowTasksSpecification(_userContext.UserId), 3),
				LaterTasks = _taskService.GetTasks(
					new LaterTasksSpecification(_userContext.UserId), 3),
				RecentlyCompletedTasks = _taskService.GetTasks(
					new CompletedTasksSpecification(_userContext.UserId), 3)
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

			_taskService.CreateTask(command.Name);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpGet]
		public IActionResult EditTask(int taskId)
		{
			var task = _taskService.GetTaskById(taskId);

			if (task == null)
				return NotFound();

			var command = new EditTaskCommand
			{
				Id = task.Id,
				Name = task.Name,
				DueDate = task.DueDate,
				PriorityId = task.Priority,
				CategoryId = task.CategoryId,
				Categories = _categoryService.GetUserCategories()
			};

			return View(command);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditTask(EditTaskCommand command)
		{
			if (!ModelState.IsValid)
			{
				command.Categories = _categoryService.GetUserCategories();
				return View(command);
			}

			var taskDto = new TaskDto
			{
				Id = command.Id,
				Name = command.Name,
				DueDate = command.DueDate,
				Priority = command.PriorityId,
				CategoryId = command.CategoryId
			};

			_taskService.UpdateTask(taskDto);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteTask(int taskId)
		{
			_taskService.DeleteTask(taskId);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ActivateTask(int taskId)
		{
			_taskService.Activate(taskId);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeactivateTask(int taskId)
		{
			_taskService.Deactivate(taskId);

			return RedirectToAction("Summary", "Tasks");
		}

		private static TasksViewModel CreateTasksViewModel(string title, IEnumerable<TaskDto> tasks)
			=> new TasksViewModel
			{
				Tasks = tasks,
				Title = title
			};
	}
}
