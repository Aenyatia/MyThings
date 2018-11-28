using MyThings.Application.Dtos;
using System.Collections.Generic;

namespace MyThings.Web.ViewModels
{
	public class EditTaskViewModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string DueDate { get; set; }

		public int PriorityId { get; set; }
		public CategoryDto Category { get; set; }

		public IEnumerable<CategoryDto> Categories { get; set; }
	}
}
