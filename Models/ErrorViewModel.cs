using System;

namespace TimeTablePlanner.Models
{
	public class ErrorViewModel
	{
		public string callId { get; set; }

		public bool showCallId => !string.IsNullOrEmpty(callId);
	}
}
