using System.Collections.Generic;
using ToDoList.Dtos;

namespace ToDoList.ViewModels
{
	public class TasksViewModel
	{
		public IEnumerable<TaskViewModel> TodayTasks { get; set; }
		public IEnumerable<TaskViewModel> TomorrowTasks { get; set; }
		public IEnumerable<TaskViewModel> LaterTasks { get; set; }
		public IEnumerable<TaskViewModel> NotDoneTasks { get; set; }
		public IEnumerable<TaskViewModel> RecentlyCompletedTasks { get; set; }
		public IEnumerable<string> Categories { get; set; }

		public int LaterTasksCount { get; set; }
		public int HistoryTasksCount { get; set; }
	}
}
