using System;
using System.Linq.Expressions;
using MyThings.Core.Domain;

namespace MyThings.Application.Specifications
{
	public interface ISpecification
	{
		Expression<Func<Task, bool>> IsSatisfiedBy { get; }
	}
}
