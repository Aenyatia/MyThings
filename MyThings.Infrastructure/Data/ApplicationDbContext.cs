using Microsoft.EntityFrameworkCore;
using MyThings.Core.Domain;
using MyThings.Infrastructure.EntityConfigurations;

namespace MyThings.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public virtual DbSet<Task> Tasks { get; protected set; }
		public virtual DbSet<Category> Categories { get; protected set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new TaskConfiguration());
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());
		}
	}
}
