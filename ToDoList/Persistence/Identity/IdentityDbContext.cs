using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Persistence.Identity
{
	public class IdentityDbContext : IdentityDbContext<ApplicationUser>
	{
		public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
			: base(options)
		{
		}
	}
}
