using System;

namespace ToDoList.ViewModels
{
	public class TaskViewModel
	{
		public int Id { get; set; }

		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime? Start { get; set; }
		public DateTime? End { get; set; }

		public bool IsFinished { get; set; }
	}
}
