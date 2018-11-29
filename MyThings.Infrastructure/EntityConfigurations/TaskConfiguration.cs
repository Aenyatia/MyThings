using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyThings.Core.Domain;

namespace MyThings.Infrastructure.EntityConfigurations
{
	public class TaskConfiguration : IEntityTypeConfiguration<Task>
	{
		public void Configure(EntityTypeBuilder<Task> builder)
		{
			builder.Property(t => t.Id)
				.IsRequired();

			builder.Property(t => t.UserId)
				.IsRequired();

			builder.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(t => t.DueDate)
				.IsRequired();

			builder.Property(t => t.Priority)
				.IsRequired();

			builder.Property(t => t.IsCompleted)
				.IsRequired();

			builder.HasOne(t => t.Category)
				.WithMany()
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasKey(t => t.Id);
			builder.ToTable("Tasks");
		}
	}
}
