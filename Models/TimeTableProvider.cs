namespace TimeTablePlanner.Models
{
	public static class TimeTableProvider
	{
		// This can be altered to yield user-specific schedules, but for now, we keep it as simple as possible
		public static TimeTable getTimeTable() => TimeTable.defaultTimeTable;
	}
}
