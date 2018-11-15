using System;
using MyThings.Core.Domain;

namespace MyThings.Application.ViewModels.Tasks
{
	public class EditTaskViewModel
	{
		public string Name { get; set; }
		public DateTime DueDate { get; set; }
		public Priority Priority { get; set; }
		public Category Category { get; set; }
	}
}
