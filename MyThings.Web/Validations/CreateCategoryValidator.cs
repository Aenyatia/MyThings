using FluentValidation;
using MyThings.Web.Commands;

namespace MyThings.Web.Validations
{
	public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
	{
		public CreateCategoryValidator()
		{
			RuleFor(x => x.Name).NotEmpty();
		}
	}
}
