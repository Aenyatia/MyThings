using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Persistence.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Task> Tasks { get; set; }
		public DbSet<Category> Categories { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	}
}
