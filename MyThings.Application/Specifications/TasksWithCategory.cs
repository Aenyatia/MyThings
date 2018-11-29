using MyThings.Core.Domain;
using System;
using System.Linq.Expressions;

namespace MyThings.Application.Specifications
{
	public class TasksWithCategory : ISpecification
	{
		public Expression<Func<Task, bool>> IsSatisfiedBy { get; }

		public TasksWithCategory(string userId, int categoryId)
		{
			IsSatisfiedBy = t => t.Category.Id == categoryId &&
								 t.UserId == userId &&
								 t.IsCompleted == false;
		}
	}
}
