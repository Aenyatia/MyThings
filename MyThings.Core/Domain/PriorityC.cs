namespace MyThings.Core.Domain
{
	public class PriorityC
	{
		public int Id { get; protected set; }
		public string Name { get; protected set; }

		protected PriorityC(string name)
		{
			Name = name;
		}

		public static PriorityC Create(string name)
			=> new PriorityC(name);
	}
}
