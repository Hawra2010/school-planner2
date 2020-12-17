using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TimeTablePlanner.Models;

namespace TimeTablePlanner.Controllers
{
	public class ErrorController : Controller
	{
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { callId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
