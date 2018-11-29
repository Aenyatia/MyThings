using FluentValidation;
using MyThings.Web.Commands;

namespace MyThings.Web.Validations
{
	public class EditTaskValidator : AbstractValidator<EditTaskCommand>
	{
		public EditTaskValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty().WithMessage("Task description should not be empty.")
				.MaximumLength(100);
		}
	}
}
