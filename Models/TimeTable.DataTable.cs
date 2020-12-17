using System;
using System.Collections.Generic;

namespace TimeTablePlanner.Models
{
	public partial class TimeTable
	{
		public sealed class DataTable : Listing<string>
		{
			public DataTable(string name) : base(name)
			{
			}

			public DataTable(string name, IList<string> list) : base(name, list)
			{
			}
		}
	}
}
