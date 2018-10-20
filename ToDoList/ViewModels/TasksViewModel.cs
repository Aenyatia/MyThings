using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
	public class TasksViewModel
	{
		public IList<Task> ActiveTasks { get; set; }
		public IList<Task> CompletedTasks { get; set; }
		public int? CategoryId { get; set; }
		public IList<Category> Categories { get; set; }
	}
}
