using MyThings.Core.Domain;
using System;
using System.Linq.Expressions;

namespace MyThings.Application.Specifications
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
