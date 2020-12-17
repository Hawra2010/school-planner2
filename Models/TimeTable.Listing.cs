using System;
using System.Collections.Generic;
using TimeTablePlanner.Collections;

namespace TimeTablePlanner.Models
{
	public partial class TimeTable
	{
		public class Listing<T> : UniqueGroup<T>, IListing
		{
			public Listing(string name)
			{
				this.name = name ?? throw new ArgumentNullException(nameof(name));
			}

			public Listing(string name, IList<T> list) : base(list)
			{
				this.name = name ?? throw new ArgumentNullException(nameof(name));
			}

			public string name { get; }
		}
	}
}
