using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

// Copyright (c) 2016 Ben Spiller. 


namespace BetterReminders
{
	public struct SnoozeTime : IComparable<SnoozeTime>
	{
		/// <summary>
		/// Number of seconds after now or after start time to wakeup
		/// </summary>
		public int Secs;
		/// <summary>
		/// if true, secs is measured from now, if false from the meeting start time
		/// </summary>
		public bool FromNow;

		public SnoozeTime(int secs, bool fromNow)
		{
			Secs = secs;
			FromNow = fromNow;
		}

		public DateTime GetNextReminderTime(DateTime startTime)
		{
			if (FromNow)
				return DateTime.Now + new TimeSpan(0, 0, Secs);
			return startTime + new TimeSpan(0, 0, Secs);
		}

		public static SnoozeTime Parse(string t)
		{
			Match m = new Regex(@"([\d.]+) *([s|h|m])").Match(t.ToLowerInvariant());
			if (!m.Success)
				throw new ArgumentException("Invalid snooze time '" + t + "': must contain <number> s|m|h");

			bool fromNow = !(t.ToLowerInvariant().Contains("start") || t.ToLowerInvariant().Contains("after") || t.ToLowerInvariant().Contains("before"));
			float secs = float.Parse(m.Groups[1].Value);
			switch (m.Groups[2].Value)
			{
				case "m": secs = secs * 60; break;
				case "h": secs = secs * 60 * 60; break;
				case "s": break;
			}
			if (secs < 1)
				throw new ArgumentException("Invalid snooze time, must be non-zero");
			if ((!t.ToLowerInvariant().Contains("after")) && !fromNow)
				secs = secs * -1;
			return new SnoozeTime(Convert.ToInt32(secs), fromNow);
		}
		public override string ToString()
		{
			string t;
			int absSecs = (Secs > 0) ? Secs : -1 * Secs;

			if (absSecs >= 60 && absSecs % 60 == 0)
				t = (absSecs / 60) + " minute" + (absSecs == 60 ? "" : "s");
			else
				t = absSecs + " second" + (absSecs == 1 ? "" : "s");
			if (FromNow)
				t = "Remind in " + t;
			else
				t = "Remind " + t + (Secs < 0 ? " before start time" : " after start time");
			// sanity check assertion
			if (!Parse(t).Equals(this)) throw new Exception("Error in snooze time ToString/Parse for: " + t);

			return t;
		}

		public static List<SnoozeTime> ParseList(string list)
		{
			return new List<SnoozeTime>(list.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => SnoozeTime.Parse(t)));
		}

		public static string ListToString(List<SnoozeTime> list)
		{
			return string.Join(",", list);
		}

		#region IComparable<SnoozeTime> Members

		public int CompareTo(SnoozeTime other)
		{
			// fromnow items first
			if (FromNow != other.FromNow)
				return FromNow ? +1 : -1;
			// small/earlier last, far times and start times at top of list
			return other.Secs - Secs;
		}

		#endregion
	}
}
