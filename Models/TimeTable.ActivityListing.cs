using System;
using System.Collections.Generic;

namespace TimeTablePlanner.Models
{
	public partial class TimeTable
	{
		public sealed class ActivityListing : Listing<Activity>
		{
			public ActivityListing() : base(name)
			{
			}

			public ActivityListing(IList<Activity> list) : base(name, list)
			{
			}

			const string name = "Activities";
		}
	}
}
