using System;
using MyThings.Core.Extensions;

namespace MyThings.Core.Domain
{
	public class Task
	{
		public int Id { get; protected set; }
		public string UserId { get; protected set; }

		public string Name { get; protected set; }
		public Priority Priority { get; protected set; }
		public DateTime DueDate { get; protected set; }
		public Category Category { get; protected set; }

		public bool IsCompleted { get; protected set; }
		public DateTime? CompletedAt { get; protected set; }

		protected Task(string userId, string name)
		{
			UserId = userId;
			Name = name;
			Priority = Priority.Default;
			DueDate = DateTime.UtcNow;
			IsCompleted = false;
			CompletedAt = null;
		}

		public static Task Create(string userId, string name)
		{
			if (userId.IsEmpty()) throw new ArgumentException();
			if (name.IsEmpty()) throw new ArgumentException();

			return new Task(userId, name);
		}

		public void Activate()
		{
			IsCompleted = false;
			CompletedAt = null;
		}

		public void Deactivate()
		{
			IsCompleted = true;
			CompletedAt = DateTime.UtcNow;
		}

		public void Edit(string name, Priority priority, DateTime dueDate, Category category)
		{
			Name = name;
			Priority = priority;
			DueDate = dueDate;
			Category = category;
		}
	}
}
