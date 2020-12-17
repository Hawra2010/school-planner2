using System;

namespace TimeTablePlanner.Models
{
	public partial class TimeTable
	{
		public interface IListing
		{
			string name { get; }
			int Count { get; }

			void Clear();
		}
	}
}
