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
	public class TaskService
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public TaskService(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public TaskDto GetTaskById(string userId, int taskId)
		{
			var task = GetTask(userId, taskId);

			return _mapper.Map<Task, TaskDto>(task);
		}

		public IEnumerable<TaskDto> GetTasksByCategory(string userId, int categoryId)
		{
			var tasks = _context.Tasks
				.Where(t => t.Category.Id == categoryId && t.UserId == userId &&
							t.IsCompleted == false)
				.ToList();

			return _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(tasks);
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

		public void CreateTask(string userId, string name)
		{
			var task = Task.Create(userId, name);

			_context.Tasks.Add(task);
			_context.SaveChanges();
		}

		public void DeleteTask(string userId, int taskId)
		{
			var task = GetTask(userId, taskId);

			if (task == null)
				throw new ArgumentException(nameof(taskId));

			_context.Tasks.Remove(task);
			_context.SaveChanges();
		}

		public void Activate(string userId, int taskId)
		{
			var task = GetTask(userId, taskId);

			if (task == null)
				throw new ArgumentException(nameof(taskId));

			task.SetActive();
			_context.SaveChanges();
		}

		public void Deactivate(string userId, int taskId)
		{
			var task = GetTask(userId, taskId);

			if (task == null)
				throw new ArgumentException(nameof(taskId));

			task.SetInactive();
			_context.SaveChanges();
		}

		public void UpdateTask(string userId, TaskDto dto)
		{
			var task = GetTask(userId, dto.Id);

			if (task == null)
				throw new ArgumentException(nameof(dto.Id));

			task.Edit(dto.Name, (Priority)dto.Priority, dto.DueDate, dto.CategoryId);

			_context.SaveChanges();
		}

		private Task GetTask(string userId, int taskId)
			=> _context.Tasks.Include(t => t.Category)
				.SingleOrDefault(t => t.Id == taskId && t.UserId == userId);
	}
}
