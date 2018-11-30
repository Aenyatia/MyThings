using System.Collections.Generic;
using MyThings.Application.Dtos;
using MyThings.Application.Specifications;

namespace MyThings.Application.Services
{
	public interface ITaskService
	{
		TaskDto GetTaskById(int taskId);
		IEnumerable<TaskDto> GetTasksByCategory(ISpecification specification);
		IEnumerable<TaskDto> GetTasks(ISpecification specification, int noOfRecords = int.MaxValue);
		TasksNumbersDto GetTasksNumbers();
		void CreateTask(string name);
		void DeleteTask(int taskId);
		void Activate(int taskId);
		void Deactivate(int taskId);
		void UpdateTask(TaskDto dto);
	}
}
