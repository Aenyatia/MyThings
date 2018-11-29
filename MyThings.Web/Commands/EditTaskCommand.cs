using MyThings.Application.Dtos;
using System;
using System.Collections.Generic;

namespace MyThings.Web.Commands
{
	public class EditTaskCommand
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime DueDate { get; set; }
		public int PriorityId { get; set; }
		public int CategoryId { get; set; }

		public IEnumerable<CategoryDto> Categories { get; set; }
	}
}
