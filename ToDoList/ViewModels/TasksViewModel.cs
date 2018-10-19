using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
	public class TasksViewModel
	{
		public IEnumerable<Task> Tasks { get; set; }
		public IEnumerable<Category> Categories { get; set; }
	}
}
