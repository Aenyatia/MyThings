using System.Collections.Generic;
using ToDoList.Dtos;

namespace ToDoList.ViewModels
{
	public class TasksCategoriesViewModel
	{
		public IEnumerable<string> Categories { get; set; }
		public TaskNumbersDto TaskCounters { get; set; }
	}
}
