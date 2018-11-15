using FluentValidation;
using MyThings.Web.Commands;

namespace MyThings.Web.Validations
{
	public class SignInValidator : AbstractValidator<SignInCommand>
	{
		public SignInValidator()
		{
			RuleFor(p => p.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(p => p.Password)
				.NotEmpty();
		}
	}
}
