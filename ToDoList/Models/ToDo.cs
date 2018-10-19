using System.Collections.Generic;

namespace ToDoList.Models
{
	public class ToDo
	{
		public int Id { get; set; }
		public string Author { get; set; }

		public ICollection<Task> Tasks { get; set; }
		public ICollection<string> Categories { get; set; }
	}
}
