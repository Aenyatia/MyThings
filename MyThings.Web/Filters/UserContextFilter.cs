using Microsoft.AspNetCore.Mvc.Filters;
using MyThings.Application.Services;
using MyThings.Infrastructure.Extensions;

namespace MyThings.Web.Filters
{
	public class UserContextFilter : IActionFilter
	{
		private readonly IUserContext _userContext;

		public UserContextFilter(IUserContext userContext)
			=> _userContext = userContext;

		public void OnActionExecuting(ActionExecutingContext context)
			=> _userContext.UserId = context.HttpContext.User.GetUserId();

		public void OnActionExecuted(ActionExecutedContext context) { }
	}
}
