using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyThings.Application.Services;
using MyThings.Infrastructure.Extensions;

namespace MyThings.Web.Controllers.Api
{
	//[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class TasksController : ControllerBase
	{
		private readonly TaskService _taskService;

		public TasksController(TaskService taskService)
		{
			_taskService = taskService;
		}

		[HttpGet("{taskId}")]
		public IActionResult Get(int taskId)
		{
			var task = _taskService.GetTaskById(User.GetUserId(), taskId);

			if (task == null)
				return NotFound();

			return Ok(task);
		}
	}
}
