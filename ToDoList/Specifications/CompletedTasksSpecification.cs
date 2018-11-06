using System;
using System.Linq.Expressions;
using Task = ToDoList.Models.Task;

namespace ToDoList.Specifications
{
	public class CompletedTasksSpecification : ISpecification
	{
		public Expression<Func<Task, bool>> IsSatisfiedBy { get; }

		public CompletedTasksSpecification(string userId)
		{
			IsSatisfiedBy = t => t.IsCompleted &&
								 t.UserId == userId;
		}
	}
}
