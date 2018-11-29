using Microsoft.AspNetCore.Mvc;
using MyThings.Application.Services;

namespace MyThings.Web.Components
{
	public class TaskViewComponent : ViewComponent
	{
		private readonly TaskService _taskService;

		public TaskViewComponent(TaskService taskService)
			=> _taskService = taskService;

		public IViewComponentResult Invoke()
			=> View("TaskViewComponent",
				_taskService.GetTasksNumbers());
	}
}
