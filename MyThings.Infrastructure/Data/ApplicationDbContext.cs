using Microsoft.EntityFrameworkCore;
using MyThings.Core.Domain;

namespace MyThings.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Task> Tasks { get; set; }
		public DbSet<Category> Categories { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Task>()
				.HasOne(t => t.Category)
				.WithMany()
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
