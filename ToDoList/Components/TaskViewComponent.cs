using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Extensions;
using ToDoList.Specifications;
using ToDoList.ViewModels.Tasks;

namespace ToDoList.Components
{
	public class TaskViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;

		public TaskViewComponent(ApplicationDbContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			var userId = HttpContext.User.GetUserId();
			return View("TaskViewComponent", new NumberOfTasksViewModel
			{
				Today = _context.Tasks.Count(new TodayTasksSpecification(userId).IsSatisfiedBy),
				Tomorrow = _context.Tasks.Count(new TomorrowTasksSpecification(userId).IsSatisfiedBy),
				Later = _context.Tasks.Count(new LaterTasksSpecification(userId).IsSatisfiedBy),
				NotDone = _context.Tasks.Count(new NotDoneTasksSpecification(userId).IsSatisfiedBy),
				Completed = _context.Tasks.Count(new CompletedTasksSpecification(userId).IsSatisfiedBy)
			});
		}
	}
}
