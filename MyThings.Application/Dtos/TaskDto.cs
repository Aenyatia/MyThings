using System;

namespace MyThings.Application.Dtos
{
	public class TaskDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime DueDate { get; set; }
		public int Priority { get; set; }
		public int? CategoryId { get; set; }
		public CategoryDto CategoryDto { get; set; }
	}
}
