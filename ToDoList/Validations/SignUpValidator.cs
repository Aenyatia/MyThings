using FluentValidation;
using ToDoList.ViewModels.Account;

namespace ToDoList.Validations
{
	public class SignUpValidator : AbstractValidator<SignUpViewModel>
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
