using System;
using ToDoList.Extensions;

namespace ToDoList.Models
{
	public class Category
	{
		public int Id { get; protected set; }
		public string UserId { get; protected set; }

		public string Name { get; protected set; }

		protected Category(string userId, string name)
		{
			UserId = userId;
			Name = name;
		}

		public static Category Create(string userId, string name)
		{
			if (userId.IsEmpty()) throw new ArgumentException();
			if (name.IsEmpty()) throw new ArgumentException();

			return new Category(userId, name);
		}
	}
}
