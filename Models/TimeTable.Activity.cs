using System;

namespace TimeTablePlanner.Models
{
	public partial class TimeTable
	{
		public sealed class Activity
		{
			public Activity(string roomName, int slot, DayOfWeek day)
			{
				room = roomName ?? throw new ArgumentNullException(nameof(roomName));
				this.slot = slot;
				this.day = day;
			}

			public string room { get; }
			public int slot { get; }
			public DayOfWeek day { get; }

			public string group { get; set; }
			public string className { get; set; }
			public string teacher { get; set; }
		}
	}
}
