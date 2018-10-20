using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ToDoList.Dtos;
using ToDoList.Extensions;
using ToDoList.Models;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Extensions;

namespace ToDoList.Controllers.Api
{
	[ApiController]
	[Route("api/[controller]")]
	public class TasksController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public TasksController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var userId = User.GetUserId();
			var dto = new TaskNumbersDto
			{
				TodayTasks = _context.Tasks.Count(t => t.DueDate.Date == DateTime.Today.Date && t.IsCompleted == false),
				TomorrowTasks = _context.Tasks.Count(t => t.DueDate.Date == DateTime.Today.AddDays(1).Date && t.IsCompleted == false),
				LaterTasks = _context.Tasks.Count(t => t.DueDate.Date > DateTime.Today.AddDays(1).Date && t.IsCompleted == false),
				NotDoneTasks = _context.Tasks.Count(t => t.DueDate.Date < DateTime.Today.Date && t.IsCompleted == false),
				HistoryTasks = _context.Tasks.Count(t => t.IsCompleted)
			};

			return Ok(dto);
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var userId = User.GetUserId();
			var task = _context.Tasks.SingleOrDefault(t => t.Id == id && t.UserId == userId);

			if (task == null)
				return NotFound();

			var taskDto = new TaskDto
			{
				DueDate = task.DueDate.ToShortDateString(),
				Priority = task.Priority.ToString(),
				Category = task.Category?.Name
			};

			return Ok(taskDto);
		}

		[HttpPost]
		public IActionResult Create([FromBody]CategoryDto dto)
		{
			if (dto.Name.IsEmpty())
				return BadRequest();

			var userId = User.GetUserId();
			var task = Task.Create(userId, dto.Name);

			_context.Tasks.Add(task);
			_context.SaveChanges();

			return Created(string.Empty, null);
		}
	}
}
