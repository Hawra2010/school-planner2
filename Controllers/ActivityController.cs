using System;
using Microsoft.AspNetCore.Mvc;
using TimeTablePlanner.Models;

namespace TimeTablePlanner.Controllers
{
	public class ActivityController : Controller
	{
		public IActionResult Index(string room, int slot, DayOfWeek day)
		{
			var timeTable = TimeTableProvider.getTimeTable();

			ViewData["Room"] = room;
			ViewData["Slot"] = slot;
			ViewData["Day"] = day;

			return View(timeTable);
		}

		[HttpPost]
		public ActionResult Append(string room, int slot, DayOfWeek day, string group, string @class, string teacher)
		{
			var timeTable = TimeTableProvider.getTimeTable();
			var activity = timeTable.GetActivityByRoomSlotAndDay(room, slot, day);
			if (activity == null)
			{
				activity = new TimeTable.Activity(room, slot, day);
				activity.teacher = teacher;
				activity.className = @class;
				activity.group = group;

				timeTable.activities.Add(activity);

				TempData["Error"] = null;
				return RedirectToAction("Index", "Room", new { room });
			}
			else
			{
				TempData["Error"] = $"An activity already exists on {day}, {timeTable.GetSlotTimeValue(slot)}, in room '{room}'";
				return RedirectToAction("Index", "Room", new { room });
			}
		}

		[HttpPost]
		public ActionResult Update(string room, int slot, DayOfWeek day, string group, string @class, string teacher)
		{
			var timeTable = TimeTableProvider.getTimeTable();
			var activity = timeTable.GetActivityByRoomSlotAndDay(room, slot, day);
			if (activity != null)
			{
				activity.teacher = teacher;
				activity.className = @class;
				activity.group = group;
				TempData["Error"] = null;
			}
			else
			{
				TempData["Error"] = $"An activity on {day}, {timeTable.GetSlotTimeValue(slot)}, in room '{room}' was not found";
			}
			return RedirectToAction("Index", "Room", new { room });
		}

		public ActionResult Remove(string room, DayOfWeek day, int slot)
		{
			var timeTable = TimeTableProvider.getTimeTable();
			var activity = timeTable.GetActivityByRoomSlotAndDay(room, slot, day);
			if (timeTable.activities.Remove(activity))
			{
				TempData["Error"] = null;
			}
			else
			{
				TempData["Error"] = $"An activity on {day}, {timeTable.GetSlotTimeValue(slot)}, in room '{room}' was not found";
			}
			return RedirectToAction("Index", "Room", new { room });
		}
	}
}
