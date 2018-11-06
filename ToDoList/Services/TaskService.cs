using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;
using ToDoList.Persistence.Data;
using ToDoList.Specifications;
using ToDoList.ViewModels.Tasks;

namespace ToDoList.Services
{
	public class TaskService
	{
		private readonly ApplicationDbContext _context;

		public TaskService(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<TaskViewModel> GetTodayTasks(string userId, int noOfRecords = int.MaxValue)
		{
			var tasks = _context.Tasks
				.Where(new TodayTasksSpecification(userId).IsSatisfiedBy)
				.OrderBy(t => t.Priority)
				.Take(noOfRecords);

			return CreateTaskViewModel(tasks);
		}

		public IEnumerable<TaskViewModel> GetTomorrowTasks(string userId, int noOfRecords = int.MaxValue)
		{
			var tasks = _context.Tasks
				.Where(new TomorrowTasksSpecification(userId).IsSatisfiedBy)
				.OrderByDescending(t => t.Priority)
				.Take(noOfRecords);

			return CreateTaskViewModel(tasks);
		}

		public IEnumerable<TaskViewModel> GetLaterTasks(string userId, int noOfRecords = int.MaxValue)
		{
			var tasks = _context.Tasks
				.Where(new LaterTasksSpecification(userId).IsSatisfiedBy)
				.OrderBy(t => t.DueDate.Date)
				.ThenByDescending(t => t.Priority)
				.Take(noOfRecords);

			return CreateTaskViewModel(tasks);
		}

		public IEnumerable<TaskViewModel> GetNotDoneTasks(string userId, int noOfRecords = int.MaxValue)
		{
			var tasks = _context.Tasks
				.Where(new NotDoneTasksSpecification(userId).IsSatisfiedBy)
				.OrderBy(t => t.Priority)
				.Take(noOfRecords);

			return CreateTaskViewModel(tasks);
		}

		public IEnumerable<TaskViewModel> GetCompletedTasks(string userId, int noOfRecords = int.MaxValue)
		{
			var tasks = _context.Tasks
				.Where(new CompletedTasksSpecification(userId).IsSatisfiedBy)
				.OrderBy(t => t.CompletedAt.Value)
				.Take(noOfRecords);

			return CreateTaskViewModel(tasks);
		}

		private static IEnumerable<TaskViewModel> CreateTaskViewModel(IQueryable<Task> tasks)
		{
			return tasks
				.Select(t => new TaskViewModel
				{
					Id = t.Id,
					Name = t.Name,
					DueDate = t.DueDate.ToLongDateString(),
					Priority = t.Priority.ToString(),
					Category = t.Category.Name
				})
				.ToList();
		}
	}
}
