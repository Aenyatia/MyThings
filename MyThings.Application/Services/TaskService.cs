using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyThings.Application.Dtos;
using MyThings.Application.Specifications;
using MyThings.Core.Domain;
using MyThings.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyThings.Application.Services
{
	public class TaskService : ITaskService
	{
		private readonly ApplicationDbContext _context;
		private readonly IUserContext _userContext;
		private readonly IMapper _mapper;

		public TaskService(ApplicationDbContext context, IUserContext userContext, IMapper mapper)
		{
			_context = context;
			_userContext = userContext;
			_mapper = mapper;
		}

		public TaskDto GetTaskById(int taskId)
		{
			var task = GetTask(taskId);

			return _mapper.Map<Task, TaskDto>(task);
		}

		public IEnumerable<TaskDto> GetTasksByCategory(ISpecification specification)
		{
			var tasks = _context.Tasks
				.Where(specification.IsSatisfiedBy)
				.ToList();

			return _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(tasks);
		}

		public IEnumerable<TaskDto> GetTasks(ISpecification specification, int noOfRecords = int.MaxValue)
		{
			var tasks = _context.Tasks
				.Where(specification.IsSatisfiedBy)
				.Take(noOfRecords)
				.ToList();

			return _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(tasks);
		}

		public TasksNumbersDto GetTasksNumbers()
		{
			return new TasksNumbersDto
			{
				Today = _context.Tasks.Count(new TodayTasksSpecification(_userContext.UserId).IsSatisfiedBy),
				Tomorrow = _context.Tasks.Count(new TomorrowTasksSpecification(_userContext.UserId).IsSatisfiedBy),
				Later = _context.Tasks.Count(new LaterTasksSpecification(_userContext.UserId).IsSatisfiedBy),
				NotDone = _context.Tasks.Count(new NotDoneTasksSpecification(_userContext.UserId).IsSatisfiedBy),
				Completed = _context.Tasks.Count(new CompletedTasksSpecification(_userContext.UserId).IsSatisfiedBy)
			};
		}

		public void CreateTask(string name)
		{
			var task = Task.Create(_userContext.UserId, name);

			_context.Tasks.Add(task);
			_context.SaveChanges();
		}

		public void DeleteTask(int taskId)
		{
			var task = GetTask(taskId);

			if (task == null)
				throw new ArgumentException(nameof(taskId));

			_context.Tasks.Remove(task);
			_context.SaveChanges();
		}

		public void Activate(int taskId)
		{
			var task = GetTask(taskId);

			if (task == null)
				throw new ArgumentException(nameof(taskId));

			task.SetActive();
			_context.SaveChanges();
		}

		public void Deactivate(int taskId)
		{
			var task = GetTask(taskId);

			if (task == null)
				throw new ArgumentException(nameof(taskId));

			task.SetInactive();
			_context.SaveChanges();
		}

		public void UpdateTask(TaskDto dto)
		{
			var task = GetTask(dto.Id);

			if (task == null)
				throw new ArgumentException(nameof(dto.Id));

			task.Edit(dto.Name, (Priority)dto.Priority, dto.DueDate, dto.CategoryId);

			_context.SaveChanges();
		}

		private Task GetTask(int taskId)
			=> _context.Tasks.Include(t => t.Category)
				.SingleOrDefault(t => t.Id == taskId && t.UserId == _userContext.UserId);
	}
}
