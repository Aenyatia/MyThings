using System.Security.Claims;

namespace ToDoList.Persistence.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static string GetUserId(this ClaimsPrincipal principal)
			=> principal.FindFirstValue(ClaimTypes.NameIdentifier);
	}
}
