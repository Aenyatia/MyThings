using System.Collections.Generic;
using ToDoList.Dtos;

namespace ToDoList.ViewModels
{
	public class TodayTasksViewModel
	{
		public IEnumerable<TaskViewModel> TodayTasks { get; set; }
		public TasksCategoriesViewModel TasksCategoriesViewModel { get; set; }
	}
}
