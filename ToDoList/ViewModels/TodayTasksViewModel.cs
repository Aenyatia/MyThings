﻿using System.Collections.Generic;
using ToDoList.Dtos;
using ToDoList.ViewModels.Tasks;

namespace ToDoList.ViewModels
{
	public class TodayTasksViewModel
	{
		public IEnumerable<TaskViewModel> TodayTasks { get; set; }
		public TasksCategoriesViewModel TasksCategoriesViewModel { get; set; }
	}
}