using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TimeTablePlanner.Models;

namespace TimeTablePlanner.Controllers
{
	public class DataController : Controller
	{
		public IActionResult Index(string table)
		{
			var timeTable = TimeTableProvider.getTimeTable();
			return View(table != null 
				? timeTable.GetDataTable(table) 
				: timeTable.dataTables.FirstOrDefault()
			);
		}

		[HttpPost]
		public ActionResult Append(string table, string item)
		{
			var timeTable = TimeTableProvider.getTimeTable();
			var dataTable = timeTable.GetDataTable(table);
			if (dataTable.Contains(item))
			{
				TempData["error"] = $"'{item}' already exists in '{table}'";
			}
			else
			{
				dataTable.Add(item);
				TempData["error"] = null;
			}
			return RedirectToAction("Index", new { table });
		}

		public ActionResult Remove(string table, string item)
		{
			var timeTable = TimeTableProvider.getTimeTable();
			var dataTable = timeTable.GetDataTable(table);
			dataTable.Remove(item);
			return RedirectToAction("Index", new { table });
		}
	}
}
