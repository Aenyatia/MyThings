using FluentValidation;
using ToDoList.Dtos;

namespace ToDoList.Validations.Categories
{
	public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
	{
		public CreateCategoryValidator()
		{
			RuleFor(c => c.Name).NotEmpty();
		}
	}
}
