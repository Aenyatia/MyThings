using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using MyThings.Application.Mappers;
using MyThings.Application.Services;
using MyThings.Core.Domain;
using MyThings.Infrastructure.Data;
using System;
using System.Linq;
using Xunit;

namespace MyThings.Tests.Services
{
	public class CategoryServiceTests
	{
		[Fact]
		public void CreateCategory_WhenCall_ShouldAddCategoryToContext()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var userContextMock = new Mock<IUserContext>();
			var mapperMock = new Mock<IMapper>();

			userContextMock.Setup(x => x.UserId).Returns("Id");

			var categoryService = new CategoryService(context, userContextMock.Object, mapperMock.Object);

			categoryService.CreateCategory("category1");

			context.Categories.Count().Should().Be(1);
		}

		[Fact]
		public void DeleteCategory_CallWithExistingId_ShouldDeleteCategoryFromContext()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var userContextMock = new Mock<IUserContext>();
			var mapperMock = new Mock<IMapper>();

			context.Categories.AddRange(
				Category.Create("id1", "name1"),
				Category.Create("id2", "name1"));
			context.SaveChanges();
			userContextMock.Setup(x => x.UserId).Returns("id1");

			var categoryService = new CategoryService(context, userContextMock.Object, mapperMock.Object);

			categoryService.DeleteCategory(context.Categories.First().Id);

			context.Categories.Count().Should().Be(1);
		}

		[Fact]
		public void DeleteCategory_CallWithFakeId_ThrowException()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var userContextMock = new Mock<IUserContext>();
			var mapperMock = new Mock<IMapper>();

			context.Categories.AddRange(
				Category.Create("id1", "name1"),
				Category.Create("id2", "name1"));
			context.SaveChanges();
			userContextMock.Setup(x => x.UserId).Returns("id1");

			var categoryService = new CategoryService(context, userContextMock.Object, mapperMock.Object);

			Action action = () => categoryService.DeleteCategory(10);

			action.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void GetUserCategories_Invoke_RetrunListOfCategories()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var userContextMock = new Mock<IUserContext>();
			var mapper = AutoMapperConfiguration.Configure();

			userContextMock.Setup(x => x.UserId)
				.Returns("id1");
			context.Categories.AddRange(
				Category.Create("id1", "name1"),
				Category.Create("id2", "name2"));
			context.SaveChanges();

			var categoryService = new CategoryService(context, userContextMock.Object, mapper);

			var result = categoryService.GetUserCategories();

			result.Count().Should().Be(1);
		}
	}
}
