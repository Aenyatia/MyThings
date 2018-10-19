using System;

namespace ToDoList.Models
{
	public class Task
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public Priority Priority { get; set; }

		public DateTime? Start { get; set; }
		public DateTime? End { get; set; }

		public bool IsCompleted { get; protected set; }

		public void Complete()
			=> IsCompleted = true;
	}
}
