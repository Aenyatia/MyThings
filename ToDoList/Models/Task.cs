using System;

namespace ToDoList.Models
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

		public void Complete()
			=> IsCompleted = true;

		public void Active()
			=> IsCompleted = false;

		protected Task(string userId, string name)
		{
			UserId = userId;
			Name = name;
			Priority = Priority.Default;
			DueDate = DateTime.UtcNow;
			IsCompleted = false;
		}

		public static Task Create(string userId, string name)
			=> new Task(userId, name);

		public void Edit(string name, Priority priority, DateTime dueDate, Category category)
		{
			Name = name;
			Priority = priority;
			DueDate = dueDate;
			Category = category;
		}
	}
}
