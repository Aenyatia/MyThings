using System.Collections.Generic;
using MyThings.Application.ViewModels.Tasks;

namespace MyThings.Application.ViewModels
{
	public class SummaryViewModel
	{
		public IEnumerable<TaskViewModel> TodayTasks { get; set; }
		public IEnumerable<TaskViewModel> TomorrowTasks { get; set; }
		public IEnumerable<TaskViewModel> LaterTasks { get; set; }
		public IEnumerable<TaskViewModel> RecentlyCompletedTasks { get; set; }
	}
}
