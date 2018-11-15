using Microsoft.AspNetCore.Mvc;
using MyThings.Application.Services;
using MyThings.Infrastructure.Extensions;

namespace MyThings.Web.Components
{
	public class TaskViewComponent : ViewComponent
	{
		private readonly TaskService _taskService;

		public TaskViewComponent(TaskService taskService)
		{
			_taskService = taskService;
		}

		public IViewComponentResult Invoke()
		{
			var userId = HttpContext.User.GetUserId();

			return View("TaskViewComponent", _taskService.GetTasksNumbers(userId));
		}
	}
}
