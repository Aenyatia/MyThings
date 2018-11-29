using MyThings.Application.Dtos;
using System.Collections.Generic;

namespace MyThings.Web.ViewModels
{
	public class TasksViewModel
	{
		public IEnumerable<TaskDto> Tasks { get; set; }
		public string Title { get; set; }
	}
}
