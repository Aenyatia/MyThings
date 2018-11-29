using FluentValidation;
using MyThings.Web.Commands;

namespace MyThings.Web.Validations
{
	public class SignUpValidator : AbstractValidator<SignUpCommand>
	{
		public SignUpValidator()
		{
			RuleFor(p => p.Email)
				.NotEmpty().WithMessage("Email is required.")
				.EmailAddress().WithMessage("Invalid email.");

			RuleFor(p => p.Password)
				.NotEmpty().WithMessage("Password is required.");

			RuleFor(p => p.ConfirmPassword)
				.Equal(p => p.Password).WithMessage("The password and confirmation password do not match.");
		}
	}
}
