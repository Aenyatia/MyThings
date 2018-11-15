using MyThings.Core.Domain;
using System;
using System.Linq.Expressions;

namespace MyThings.Application.Specifications
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
