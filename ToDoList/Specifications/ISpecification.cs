using System;
using System.Linq.Expressions;
using ToDoList.Models;

namespace ToDoList.Specifications
{
	public interface ISpecification
	{
		Expression<Func<Task, bool>> IsSatisfiedBy { get; }
	}
}
