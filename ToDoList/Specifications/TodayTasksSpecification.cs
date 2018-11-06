using System;
using System.Linq.Expressions;
using Task = ToDoList.Models.Task;

namespace ToDoList.Specifications
{
	public class TodayTasksSpecification : ISpecification
	{
		public Expression<Func<Task, bool>> IsSatisfiedBy { get; }

		public TodayTasksSpecification(string userId)
		{
			IsSatisfiedBy = t => t.DueDate.Date == DateTime.Today.Date &&
								 t.IsCompleted == false &&
								 t.UserId == userId;
		}
	}
}
