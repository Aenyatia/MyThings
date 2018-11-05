using Microsoft.AspNetCore.Mvc;
using ToDoList.Persistence.Data;
using ToDoList.ViewModels.Tasks;

namespace ToDoList.Components
{
	public class TasksViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;

		public TasksViewComponent(ApplicationDbContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			return View("TasksViewComponent", new NumberOfTasksViewModel
			{
				Today = 1,
				Tomorrow = 2,
				Later = 3,
				NotDone = 4,
				History = 5
			});
		}
	}
}
