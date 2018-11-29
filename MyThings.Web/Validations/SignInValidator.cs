using FluentValidation;
using MyThings.Web.Commands;

namespace MyThings.Web.Validations
{
	public class SignInValidator : AbstractValidator<SignInCommand>
	{
		public SignInValidator()
		{
			RuleFor(p => p.Email)
				.NotEmpty().WithMessage("Email is required.")
				.EmailAddress().WithMessage("Invalid email.");

			RuleFor(p => p.Password)
				.NotEmpty().WithMessage("Password is required.");
		}
	}
}
