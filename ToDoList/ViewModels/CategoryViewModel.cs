using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
	public class CategoryViewModel
	{
		public IEnumerable<Category> Categories { get; set; }
	}
}
