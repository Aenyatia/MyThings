using System;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
	public class EditTaskViewModel
	{
		public string Name { get; set; }
		public DateTime DueDate { get; set; }
		public Priority Priority { get; set; }
		public Category Category { get; set; }
	}
}
