using System.Collections.Generic;
using MyThings.Application.Dtos;

namespace MyThings.Web.Commands
{
	public class EditTaskCommand
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string DueDate { get; set; }

		public int PriorityId { get; set; }
		public CategoryDto Category { get; set; }

		public IEnumerable<CategoryDto> Categories { get; set; }
	}
}
