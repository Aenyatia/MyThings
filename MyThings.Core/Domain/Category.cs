using System;

namespace MyThings.Core.Domain
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
			if (userId == null) throw new ArgumentNullException(nameof(userId));
			if (name == null) throw new ArgumentNullException(nameof(name));

			return new Category(userId, name);
		}
	}
}
