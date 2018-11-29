using FluentValidation;
using MyThings.Web.Commands;

namespace MyThings.Web.Validations
{
	public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
	{
		public CreateCategoryValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty().WithMessage("Category name should not be empty.")
				.MaximumLength(50);
		}
	}
}
