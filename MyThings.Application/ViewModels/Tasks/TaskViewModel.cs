using System;

namespace MyThings.Application.ViewModels.Tasks
{
	public class TaskViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string DueDate { get; set; }
		public string Priority { get; set; }
		public string Category { get; set; }

		public string PriorityClass()
		{
			switch (Priority)
			{
				case "Default":
					return string.Empty;
				case "Low":
					return "list-group-item-success";
				case "Medium":
					return "list-group-item-warning";
				case "High":
					return "list-group-item-danger";
				default:
					throw new ArgumentException();
			}
		}
	}
}
