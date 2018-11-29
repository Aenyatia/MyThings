using System;

namespace MyThings.Core.Domain
{
	public class Task
	{
		public int Id { get; protected set; }
		public string UserId { get; protected set; }

		public string Name { get; protected set; }
		public Priority Priority { get; protected set; }
		public DateTime DueDate { get; protected set; }

		public int? CategoryId { get; protected set; }
		public Category Category { get; protected set; }

		public bool IsCompleted { get; protected set; }
		public DateTime? CompletedAt { get; protected set; }

		protected Task(string userId, string name)
		{
			UserId = userId;
			Name = name;
			Priority = Priority.NoPriority;
			DueDate = DateTime.Now;
			IsCompleted = false;
			CompletedAt = null;
		}

		public static Task Create(string userId, string name)
			=> new Task(userId, name);

		public void SetActive()
		{
			if (!IsCompleted)
				return;

			IsCompleted = false;
			CompletedAt = null;
		}

		public void SetInactive()
		{
			if (IsCompleted)
				return;

			IsCompleted = true;
			CompletedAt = DateTime.Now;
		}

		public void Edit(string name, Priority priority, DateTime dueDate, int? categoryId)
		{
			Name = name;
			Priority = priority;
			DueDate = dueDate;
			CategoryId = categoryId;
		}
	}
}
