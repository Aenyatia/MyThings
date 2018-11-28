using Microsoft.AspNetCore.Mvc;
using MyThings.Application.Dtos;
using MyThings.Application.Services;
using MyThings.Application.Specifications;
using MyThings.Infrastructure.Extensions;
using MyThings.Web.Commands;
using MyThings.Web.ViewModels;

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
		public IActionResult DeleteTask(int taskId)
		{
			_taskService.RemoveTask(User.GetUserId(), taskId);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpPost]
		public IActionResult Complete(int taskId)
		{
			_taskService.Deactivate(User.GetUserId(), taskId);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpPost]
		public IActionResult Active(int taskId)
		{
			_taskService.Activate(User.GetUserId(), taskId);

			return RedirectToAction("Summary", "Tasks");
		}

		[HttpGet]
		public IActionResult TodayTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new TodayTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet]
		public IActionResult TomorrowTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new TomorrowTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet]
		public IActionResult LaterTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new LaterTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet]
		public IActionResult NotDoneTasks()
		{
			var userId = User.GetUserId();
			var tasks = _taskService.GetTasks(new NotDoneTasksSpecification(userId), null);

			return View("Tasks", tasks);
		}

		[HttpGet]
		public IActionResult CompletedTasks()
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
		public IActionResult Edit(int taskId)
		{
			var task = _taskService.GetTaskById(User.GetUserId(), taskId);

			if (task == null)
				return NotFound();

			var vm = new EditTaskViewModel
			{
				Id = task.Id,
				Name = task.Name,
				DueDate = task.DueDate.ToString("yyyy-MM-dd"),
				PriorityId = task.Priority,
				Category = task.Category,
				Categories = _categoryService.GetUserCategories(User.GetUserId())
			};

			return View(vm);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Edit(TaskDto viewModel)
		{
			if (!ModelState.IsValid)
				return View();


			return RedirectToAction("Summary", "Tasks");
		}
	}
}
