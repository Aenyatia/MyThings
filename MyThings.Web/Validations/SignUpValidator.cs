using FluentValidation;
using MyThings.Web.Commands;

namespace MyThings.Web.Validations
{
	public class SignUpValidator : AbstractValidator<SignUpCommand>
	{
		public SignUpValidator()
		{
			RuleFor(p => p.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(p => p.Password)
				.NotEmpty();

			RuleFor(p => p.ConfirmPassword)
				.Equal(p => p.Password).WithMessage("The password and confirmation password do not match.");
		}
	}
}
