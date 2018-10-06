using FluentValidation;
using ToDoList.ViewModels;

namespace ToDoList.Validations
{
	public class SignInValidator : AbstractValidator<SignInViewModel>
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
