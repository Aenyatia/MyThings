using FluentAssertions;
using MyThings.Core.Domain;
using System;
using Xunit;

namespace MyThings.Tests
{
	public class TaskTests
	{
		[Fact]
		public void Create_WhenUserIdAndNameAreValid_ReturnTaskObject()
		{
			var task = Task.Create("UserId", "Name");

			task.Should().BeAssignableTo<Task>();
			task.UserId.Should().Be("UserId");
			task.Name.Should().Be("Name");
		}

		[Fact]
		public void Create_WhenUserIdIsNull_ThrowException()
		{
			Func<Task> func = () => Task.Create(null, "Name");

			func.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Create_WhenNameIsNull_ThrowException()
		{
			Func<Task> func = () => Task.Create("UserId", null);

			func.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void SetInactive_WhenIsCompletedIsFalse_ShouldChangeValueToTrueAndSetCompletedAt()
		{
			var task = Task.Create("UserId", "Name");

			task.SetInactive();

			task.IsCompleted.Should().BeTrue();
			task.CompletedAt.Should().HaveValue();
		}

		[Fact]
		public void SetInactive_WhenIsCompletedIsTrue_ThrowException()
		{
			var task = Task.Create("UserId", "Name");

			task.SetInactive();
			Action action = () => task.SetInactive();

			action.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public void SetActive_WhenIsCompletedIsTrue_ShouldChangeValueToFalseAndSetCompletedAtEqualsNull()
		{
			var task = Task.Create("UserId", "Name");

			task.SetInactive();
			task.SetActive();

			task.IsCompleted.Should().BeFalse();
			task.CompletedAt.Should().BeNull();
		}

		[Fact]
		public void SetActive_WhenIsCompletedIsFalse_ThrowException()
		{
			var task = Task.Create("UserId", "Name");

			Action action = () => task.SetActive();

			action.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public void Edit_WhenNameIsNull_ThrowException()
		{
			var task = Task.Create("UserId", "Name");

			Action action = () => task.Edit(null, Priority.NoPriority, DateTime.Now, null);

			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Edit_WhenArgumentsAreValid_ShouldUpdateTaskProperties()
		{
			var task = Task.Create("UserId", "Name");

			task.Edit("NewName", Priority.High, new DateTime(2018, 1, 1), 1);

			task.Name.Should().Be("NewName");
			task.Priority.Should().Be(Priority.High);
			task.DueDate.Should().Be(new DateTime(2018, 1, 1));
			task.CategoryId.Should().HaveValue();
			task.CategoryId.Should().Be(1);
		}
	}
}
