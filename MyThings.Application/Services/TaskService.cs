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

		public void CreateTask(string userId, string name)
		{
			var task = Task.Create(userId, name);

			_context.Tasks.Add(task);
			_context.SaveChanges();
		}

		public void RemoveTask(string userId, int taskId)
		{
			var task = _context.Tasks.SingleOrDefault(t => t.Id == taskId && t.UserId == userId);

			if (task == null)
				return;

			_context.Tasks.Remove(task);
			_context.SaveChanges();
		}

		public void Activate(string userId, int taskId)
		{
			var task = _context.Tasks.SingleOrDefault(t => t.Id == taskId && t.UserId == userId);
			if (task == null)
				throw new ArgumentNullException();

			task.SetActive();
			_context.SaveChanges();
		}

		public void Deactivate(string userId, int taskId)
		{
			var task = _context.Tasks.SingleOrDefault(t => t.Id == taskId && t.UserId == userId);
			if (task == null)
				throw new ArgumentNullException();

			task.SetInactive();
			_context.SaveChanges();
		}

		public TaskDto GetTaskById(string userId, int taskId)
		{
			var task = _context.Tasks.Include(t => t.Category).SingleOrDefault(t => t.Id == taskId && t.UserId == userId);

			return _mapper.Map<Task, TaskDto>(task);
		}

		public void UpdateTask(TaskDto dto)
		{
			//var userId = User.GetUserId();
			//var gig = _context.Gigs
			//	.Include(g => g.Attendances).ThenInclude(a => a.Attendee)
			//	.SingleOrDefault(g => g.Id == viewModel.Id && g.ArtistId == userId);

			//if (gig == null)
			//	return NotFound();

			//gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.GenreId);

			//_context.SaveChanges();
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

		public IEnumerable<TaskDto> GetTasksByCategory(string userId, int categoryId)
		{
			var tasks = _context.Tasks
				.Where(t => t.Category.Id == categoryId && t.UserId == userId)
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
