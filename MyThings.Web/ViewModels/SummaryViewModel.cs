using MyThings.Application.Dtos;
using System.Collections.Generic;

namespace MyThings.Web.ViewModels
{
	public class SummaryViewModel
	{
		public IEnumerable<TaskDto> TodayTasks { get; set; }
		public IEnumerable<TaskDto> TomorrowTasks { get; set; }
		public IEnumerable<TaskDto> LaterTasks { get; set; }
		public IEnumerable<TaskDto> RecentlyCompletedTasks { get; set; }
	}
}
