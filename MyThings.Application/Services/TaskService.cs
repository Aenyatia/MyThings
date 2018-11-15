using AutoMapper;
using MyThings.Application.Dtos;
using MyThings.Application.Specifications;
using MyThings.Core.Domain;
using MyThings.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyThings.Application.Services
{
	public class TaskService
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public TaskService(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public void CreateTask(string userId, string name)
		{
			var task = Task.Create(userId, name);

			_context.Tasks.Add(task);
			_context.SaveChanges();
		}

		public void RemoveTask(string userId, int taskId)
		{
			var task = _context.Tasks.SingleOrDefault(t => t.Id == taskId && t.UserId == userId);

			if (task != null)
				_context.Tasks.Remove(task);
		}

		public void ActivateTask(string userId, int taskId)
		{
			var task = _context.Tasks.SingleOrDefault(t => t.Id == taskId && t.UserId == userId);
			if (task == null)
				throw new ArgumentNullException();

			task.Activate();
			_context.SaveChanges();
		}

		public void DeactivateTask(string userId, int taskId)
		{
			var task = _context.Tasks.SingleOrDefault(t => t.Id == taskId && t.UserId == userId);
			if (task == null)
				throw new ArgumentNullException();

			task.Deactivate();
			_context.SaveChanges();
		}

		public IEnumerable<TaskDto> GetTasks(ISpecification specification, int? noOfRecords)
		{
			var number = noOfRecords ?? int.MaxValue;

			var tasks = _context.Tasks
				.Where(specification.IsSatisfiedBy)
				.Take(number)
				.ToList();

			return _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(tasks);
		}

		public TasksNumbersDto GetTasksNumbers(string userId)
		{
			return new TasksNumbersDto
			{
				Today = _context.Tasks.Count(new TodayTasksSpecification(userId).IsSatisfiedBy),
				Tomorrow = _context.Tasks.Count(new TomorrowTasksSpecification(userId).IsSatisfiedBy),
				Later = _context.Tasks.Count(new LaterTasksSpecification(userId).IsSatisfiedBy),
				NotDone = _context.Tasks.Count(new NotDoneTasksSpecification(userId).IsSatisfiedBy),
				Completed = _context.Tasks.Count(new CompletedTasksSpecification(userId).IsSatisfiedBy)
			};
		}
	}
}
