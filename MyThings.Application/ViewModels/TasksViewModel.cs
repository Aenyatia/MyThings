using System.Collections.Generic;
using MyThings.Application.ViewModels.Tasks;

namespace MyThings.Application.ViewModels
{
	public class TasksViewModel
	{
		public string TaskOption { get; set; }
		public IEnumerable<TaskViewModel> Tasks { get; set; }
	}
}
