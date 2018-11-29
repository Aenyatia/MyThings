using MyThings.Application.Dtos;
using System;
using System.Collections.Generic;

namespace MyThings.Web.Commands
{
	public class EditTaskCommand
	{
		private int? _categoryId;

		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime DueDate { get; set; }
		public int PriorityId { get; set; }
		public int? CategoryId
		{
			get => _categoryId.HasValue && _categoryId.Value != 0 ? _categoryId : null;
			set => _categoryId = value;
		}

		public IEnumerable<CategoryDto> Categories { get; set; }
	}
}
