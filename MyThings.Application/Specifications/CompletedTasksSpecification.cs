using MyThings.Core.Domain;
using System;
using System.Linq.Expressions;

namespace MyThings.Application.Specifications
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
