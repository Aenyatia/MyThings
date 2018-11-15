using FluentValidation;
using MyThings.Web.Commands;

namespace MyThings.Web.Validations
{
	public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
	{
		public CreateTaskValidator()
		{
		}
	}
}
