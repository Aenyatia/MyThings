﻿using Microsoft.AspNetCore.Mvc;
using ToDoList.Persistence.Extensions;
using ToDoList.Services;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
	public class HomeController : Controller
	{
		private readonly TaskService _taskService;

		public HomeController(TaskService taskService)
		{
			_taskService = taskService;
		}

		public IActionResult Index()
		{
			var userId = User.GetUserId();
			var summaryViewModel = new SummaryViewModel
			{
				TodayTasks = _taskService.GetTodayTasks(userId, 5),
				TomorrowTasks = _taskService.GetTomorrowTasks(userId, 5),
				LaterTasks = _taskService.GetLaterTasks(userId, 5),
				RecentlyCompletedTasks = _taskService.GetCompletedTasks(userId, 5)
			};

			return View(summaryViewModel);
		}
	}
}
