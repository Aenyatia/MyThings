using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyThings.Infrastructure.Identity
{
	public class IdentityDbContext : IdentityDbContext<ApplicationUser>
	{
		public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
			: base(options)
		{
		}
	}
}
