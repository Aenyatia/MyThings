using FluentAssertions;
using MyThings.Core.Domain;
using System;
using Xunit;

namespace MyThings.Tests
{
	public class CategoryTests
	{
		[Fact]
		public void Create_WhenUserIdAndNameAreValid_ReturnCategoryObject()
		{
			var category = Category.Create("UserId", "Name");

			category.Should().BeAssignableTo<Category>();
			category.UserId.Should().Be("UserId");
			category.Name.Should().Be("Name");
		}

		[Fact]
		public void Create_WhenUserIdIsNull_ThrowException()
		{
			Func<Category> func = () => Category.Create(null, "Name");

			func.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Create_WhenUserNameIsNull_ThrowException()
		{
			Func<Category> func = () => Category.Create("Id", null);

			func.Should().Throw<ArgumentNullException>();
		}
	}
}
