using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TimeTablePlanner.Models;

namespace TimeTablePlanner.Controllers
{
    public class FileController : Controller
    {
        public IActionResult Load(string data = null)
        {
            var timeTable = TimeTableProvider.getTimeTable();
            if (data != null)
            {
                timeTable.ClearTimeTable();
                var dataObject = JObject.Parse(data);
                var rooms = dataObject["rooms"];
                var groups = dataObject["groups"];
                var classes = dataObject["classes"];
                var teachers = dataObject["teachers"];

                foreach (var value in rooms)
                    timeTable.rooms.Add(value.Value<string>());
                foreach (var value in groups)
                    timeTable.groups.Add(value.Value<string>());
                foreach (var value in classes)
                    timeTable.classes.Add(value.Value<string>());
                foreach (var value in teachers)
                    timeTable.teachers.Add(value.Value<string>());

                var activities = dataObject["activities"];
                foreach (var value in activities)
                {
                    var room = value["room"].Value<string>();
                    var slot = value["slot"].Value<int>();
                    var day = (DayOfWeek)value["day"].Value<int>();
                    var activity = new TimeTable.Activity(room, slot, day)
                    {
                        group = value["group"].Value<string>(),
                        className = value["class"].Value<string>(),
                        teacher = value["teacher"].Value<string>()
                    };
                    timeTable.activities.Add(activity);
                }

                return RedirectToAction("Index", "Room");
            }
            else
            {
                return View(timeTable);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var timeTable = TimeTableProvider.getTimeTable();
            if (file != null && file.Length > 0)
            {
                var data = new StringBuilder();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        data.AppendLine(await reader.ReadLineAsync());
                }

                if (data != null)
                {
                    var dataObject = JObject.Parse(data.ToString());
                    if (dataObject.ContainsKey("rooms") && dataObject.ContainsKey("groups") && dataObject.ContainsKey("classes") && dataObject.ContainsKey("teachers") && dataObject.ContainsKey("activities"))
                    {
                        timeTable.ClearTimeTable();
                        var rooms = dataObject["rooms"];
                        var groups = dataObject["groups"];
                        var classes = dataObject["classes"];
                        var teachers = dataObject["teachers"];

                        foreach (var value in rooms)
                            timeTable.rooms.Add(value.Value<string>());
                        foreach (var value in groups)
                            timeTable.groups.Add(value.Value<string>());
                        foreach (var value in classes)
                            timeTable.classes.Add(value.Value<string>());
                        foreach (var value in teachers)
                            timeTable.teachers.Add(value.Value<string>());

                        var activities = dataObject["activities"];
                        foreach (var value in activities)
                        {
                            var room = value["room"].Value<string>();
                            var slot = value["slot"].Value<int>();
                            var day = (DayOfWeek)value["day"].Value<int>();
                            var activity = new TimeTable.Activity(room, slot, day)
                            {
                                group = value["group"].Value<string>(),
                                className = value["className"].Value<string>(),
                                teacher = value["teacher"].Value<string>()
                            };
                            timeTable.activities.Add(activity);
                        }

                        return RedirectToAction("Index", "Room");
                    }
                }
            }
            return View(timeTable);
        }

        public IActionResult Save()
        {
            var timeTable = TimeTableProvider.getTimeTable();
            var json = JsonConvert.SerializeObject(new
            {
                rooms = timeTable.rooms,
                groups = timeTable.groups,
                classes = timeTable.classes,
                teachers = timeTable.teachers,
                activities = timeTable.activities
            }, Formatting.Indented);
            ViewBag.JsonData = json;
            return View(timeTable);
        }

        public IActionResult ClearTimeTableRequest()
        {
            return View();
        }

        public IActionResult ClearTimeTableComplete()
        {
            return View();
        }

        public IActionResult ClearTimeTable()
        {
            var clearTimeTable = TimeTableProvider.getTimeTable();
            clearTimeTable.ClearTimeTable();
            return RedirectToAction("ClearTimeTableComplete");
        }
    }
}
