using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TimeTablePlanner.Models;

namespace TimeTablePlanner.Controllers
{
	public class RoomController : Controller
	{
		public IActionResult Index(string room)
		{
			var timeTable = TimeTableProvider.getTimeTable();
			if (timeTable.rooms.Contains(room))
			{
				ViewData["ActiveRoom"] = room;
			}
			else
			{
				ViewData["ActiveRoom"] = timeTable.rooms.FirstOrDefault();
			}
			return View(timeTable);
		}
	}
}
