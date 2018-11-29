using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyThings.Core.Domain;

namespace MyThings.Infrastructure.EntityConfigurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.Property(c => c.Id)
				.IsRequired();

			builder.Property(c => c.UserId)
				.IsRequired();

			builder.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(50);

			builder.HasKey(c => c.Id);
			builder.ToTable("Categories");
		}
	}
}
