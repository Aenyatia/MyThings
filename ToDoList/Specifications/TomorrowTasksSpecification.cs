using System;
using System.Linq.Expressions;
using Task = ToDoList.Models.Task;

namespace ToDoList.Specifications
{
	public class TomorrowTasksSpecification : ISpecification
	{
		public Expression<Func<Task, bool>> IsSatisfiedBy { get; }

		public TomorrowTasksSpecification(string userId)
		{
			IsSatisfiedBy = t => t.DueDate.Date == DateTime.Today.AddDays(1).Date &&
			                     t.IsCompleted == false &&
			                     t.UserId == userId;
		}
	}
}
