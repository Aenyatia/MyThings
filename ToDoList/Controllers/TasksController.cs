﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ToDoList.Dtos;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Extensions;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
	public class TasksController : Controller
	{
		private readonly ApplicationDbContext _context;

		public TasksController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var task = _context.Tasks.SingleOrDefault(t => t.Id == id);
			if (task == null)
				return NotFound();

			return Ok(task);
		}

		[HttpGet]
		public IActionResult Edit()
		{
			return View("EditTask");
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

			task.Complete();
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

			task.Active();
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}
	}
}