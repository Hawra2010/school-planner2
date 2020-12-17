using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TimeTablePlanner.Models
{
	public sealed partial class TimeTable : IEnumerable<TimeTable.IListing>
	{
		private readonly Dictionary<string, IListing> timeTables;

		private TimeTable()
		{
			timeTables = new Dictionary<string, IListing>()
			{
				{ nameof(rooms), rooms = new DataTable(nameof(rooms)) },
				{ nameof(groups), groups = new DataTable(nameof(groups)) },
				{ nameof(classes), classes = new DataTable(nameof(classes)) },
				{ nameof(teachers), teachers = new DataTable(nameof(teachers)) },
				{ nameof(activities), activities = new ActivityListing() }
			};

			// Default Values
			rooms.Add("110");
			rooms.Add("111");
			rooms.Add("120");
			rooms.Add("121");
			groups.Add("1a");
			groups.Add("1b");
			groups.Add("1c");
			groups.Add("2a");
			groups.Add("2b");
			groups.Add("3a");
			groups.Add("3b");
			groups.Add("4a");
			groups.Add("4b");
			classes.Add("mat");
			classes.Add("geo");
			classes.Add("eng");
			classes.Add("phys");
			classes.Add("biol");
			teachers.Add("kowalski");
			teachers.Add("nowak");
			teachers.Add("smith");
			teachers.Add("clarkson");
			teachers.Add("may");
			teachers.Add("hammond");
			teachers.Add("atkinson");
		}

		public void ClearTimeTable()
		{
			foreach (var table in this)
			{
				table.Clear();
			}
		}

		public DataTable GetDataTable(string name)
		{
			return dataTables.FirstOrDefault(x => x.name == name);
		}

		public IEnumerable<Activity> GetActivitiesByRoom(string roomName)
		{
			return activities.Where(x => x.room == roomName);
		}

		public IEnumerable<Activity> GetActivitiesByRoomAndDay(string roomName, DayOfWeek day)
		{
			return GetActivitiesByRoom(roomName).Where(x => x.day == day);
		}

		public Activity GetActivityByRoomSlotAndDay(string roomName, int slot, DayOfWeek day)
		{
			return GetActivitiesByRoomAndDay(roomName, day).FirstOrDefault(x => x.slot == slot);
		}

		public string GetSlotTimeValue(int slot)
		{
			TimeSpan slotStartTime = GetSlotStartTime(slot);
			TimeSpan slotEndTime = GetSlotEndTime(slot);
			string slotStartTimeHours = slotStartTime.Hours == 0? "00" : slotStartTime.Hours.ToString();
			string slotStartTimeMinutes = slotStartTime.Minutes == 0? "00" : slotStartTime.Minutes.ToString();
			string slotEndTimeHours = slotEndTime.Hours == 0? "00" : slotEndTime.Hours.ToString();
			string slotEndTimeMinutes = slotEndTime.Minutes == 0? "00" : slotEndTime.Minutes.ToString();
			return $"{slotStartTimeHours}:{slotStartTimeMinutes} - {slotEndTimeHours}:{slotEndTimeMinutes}";
		}

		public TimeSpan GetSlotStartTime(int slot)
		{
			return slotStartTime + slot * slotTime + slot * breakTime;
		}

		public TimeSpan GetSlotEndTime(int slot)
		{
			return GetSlotStartTime(slot) + slotTime;
		}

		public int slots { get; set; } = 12;
		public TimeSpan breakTime { get; set; } = new TimeSpan(0, 15, 00);
		public TimeSpan slotTime { get; set; } = new TimeSpan(0, 45, 00);
		public TimeSpan slotStartTime { get; set; } = new TimeSpan(8, 15, 00);
		public DayOfWeek[] validDays { get; set; } = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

		public DataTable rooms { get; }
		public DataTable groups { get; }
		public DataTable classes { get; }
		public DataTable teachers { get; }
		public ActivityListing activities { get; }

		public IEnumerable<DataTable> dataTables
			=> timeTables.Values.OfType<DataTable>();

		public IEnumerator<IListing> GetEnumerator()
		{
			return timeTables.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return timeTables.Values.GetEnumerator();
		}

		public static TimeTable defaultTimeTable { get; } = new TimeTable();
	}
}
