using Microsoft.AspNetCore.Mvc;
using MyThings.Application.Services;
using MyThings.Application.Specifications;
using MyThings.Infrastructure.Extensions;
using MyThings.Web.Commands;

namespace MyThings.Web.Controllers
{
	public class TasksController : Controller
	{
		private readonly TaskService _taskService;

		public TasksController(TaskService taskService)
		{
			_taskService = taskService;
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

			return RedirectToAction("", "");
		}

		[HttpDelete]
		public IActionResult DeleteTask(int taskId)
		{
			_taskService.RemoveTask(User.GetUserId(), taskId);

			return RedirectToAction("", "");
		}

		[HttpPost]
		public IActionResult Complete(int taskId)
		{
			_taskService.DeactivateTask(User.GetUserId(), taskId);

			return RedirectToAction("", "");
		}

		[HttpPost]
		public IActionResult Active(int taskId)
		{
			_taskService.ActivateTask(User.GetUserId(), taskId);

			return RedirectToAction("", "");
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
	}
}
