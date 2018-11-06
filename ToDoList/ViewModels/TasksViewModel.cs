using System.Collections.Generic;
using ToDoList.ViewModels.Tasks;

namespace ToDoList.ViewModels
{
	public class TasksViewModel
	{
		public string TaskOption { get; set; }
		public IEnumerable<TaskViewModel> Tasks { get; set; }
	}
}
