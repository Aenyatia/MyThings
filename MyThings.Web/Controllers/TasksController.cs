using Microsoft.AspNetCore.Mvc;
using MyThings.Application.Dtos;
using MyThings.Application.Services;
using MyThings.Application.Specifications;
using MyThings.Infrastructure.Extensions;
using MyThings.Web.Commands;
using MyThings.Web.ViewModels;
using System;

namespace MyThings.Web.Controllers
{
	public class TasksController : Controller
	{
		private readonly TaskService _taskService;
		private readonly CategoryService _categoryService;

		public TasksController(TaskService taskService, CategoryService categoryService)
		{
			_taskService = taskService;
			_categoryService = categoryService;
		}

		[HttpGet]
		public IActionResult CreateTask()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateTask(CreateTaskCommand command)
		{
			if (!ModelState.IsValid)
				return View(command);

			_taskService.CreateTask(User.GetUserId(), command.Name);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteTask(int taskId)
		{
			_taskService.RemoveTask(User.GetUserId(), taskId);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpPost]
		public IActionResult DeactivateTask(int taskId)
		{
			_taskService.Deactivate(User.GetUserId(), taskId);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpPost]
		public IActionResult ActivateTask(int taskId)
		{
			_taskService.Activate(User.GetUserId(), taskId);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpGet("tasks/today")]
		public IActionResult GetTodayTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new TodayTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet]
		public IActionResult GetTomorrowTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new TomorrowTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet]
		public IActionResult GetLaterTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new LaterTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet]
		public IActionResult GetUndoneTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new NotDoneTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet]
		public IActionResult GetCompletedTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new CompletedTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet]
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
		public IActionResult GetTasksByCategory(int categoryId)
		{
			var tasks = _taskService.GetTasksByCategory(User.GetUserId(), categoryId);

			return View("Tasks", tasks);
		}

		[HttpGet]
		public IActionResult EditTask(int taskId)
		{
			var task = _taskService.GetTaskById(User.GetUserId(), taskId);

			if (task == null)
				return NotFound();

			var vm = new EditTaskCommand
			{
				Id = task.Id,
				Name = task.Name,
				DueDate = task.DueDate.ToString("yyyy-MM-dd"),
				PriorityId = task.Priority,
				CategoryId = task.Category.Id,
				Categories = _categoryService.GetUserCategories(User.GetUserId())
			};

			return View(vm);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult EditTask(EditTaskCommand command)
		{
			if (!ModelState.IsValid)
			{
				command.Categories = _categoryService.GetUserCategories(User.GetUserId());

				return View();
			}

			var taskd = new TaskDto
			{
				//Id = command.Id,
				//Priority = command.PriorityId,
				//Name = command.Name,
				//Category = new CategoryDto
				//{
				//	Id = command.Category.Id,
				//	Name = command.Category.Name
				//},
				//DueDate = DateTime.Parse(command.DueDate)
			};

			_taskService.UpdateTask(User.GetUserId(), taskd);

			return RedirectToAction("Summary", "Tasks");
		}

		public IActionResult GetTaskDetails(int taskId)
		{
			throw new NotImplementedException();
		}
	}
}
