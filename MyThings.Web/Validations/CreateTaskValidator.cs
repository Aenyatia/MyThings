using FluentValidation;
using MyThings.Web.Commands;

namespace MyThings.Web.Validations
{
	public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
	{
		public CreateTaskValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty().WithMessage("Task description should not be empty.")
				.MaximumLength(100);
		}
	}
}
