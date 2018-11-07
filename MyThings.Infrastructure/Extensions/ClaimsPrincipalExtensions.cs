using System.Security.Claims;

namespace MyThings.Infrastructure.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static string GetUserId(this ClaimsPrincipal principal)
			=> principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	}
}
