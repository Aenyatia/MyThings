using System;
using System.Linq.Expressions;
using Task = ToDoList.Models.Task;

namespace ToDoList.Specifications
{
	public class NotDoneTasksSpecification : ISpecification
	{
		public Expression<Func<Task, bool>> IsSatisfiedBy { get; }

		public NotDoneTasksSpecification(string userId)
		{
			IsSatisfiedBy = t => t.DueDate.Date < DateTime.Today.Date &&
								 t.IsCompleted == false &&
								 t.UserId == userId;
		}
	}
}
