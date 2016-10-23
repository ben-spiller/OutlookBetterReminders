using System;
using System.IO;
using System.Threading;
namespace BetterReminders
{
	/// <summary>
	/// A trivial class for diagnostic logging to a text file in usuer=profiledir/AppData/Local/OutlookBetterReminders; 
	/// also writes to the standard Diagnostics.Trace logger as a backup, though that's not so easy to use from an addin. 
	/// 
	/// Since most people won't want a log, we only generate one if the file already exists
	/// 
	/// Currently the log level is not configurable, but as we wipe the log on startup we should never generate enough output to be a problem. 
	/// </summary>
	class Logger
	{
		public static Logger GetLogger() { return instance; }

		private static Logger instance = new Logger();
		private StreamWriter sw = null;
		private Logger()
		{
			try
			{
				string logpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\OutlookBetterReminders\better-reminders.log";
				if (!File.Exists(logpath))
				{
					// we still have diagnotics.trace logging
					Info("Not writing a .log file as " + logpath + " does not exist");
					return;
				}
				// create a new text file, overwrite existing
				sw = new StreamWriter(new FileStream(logpath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
				{
					AutoFlush = true
				};
			} catch (Exception ex)
			{
				Error("Failed to setup logger: ", ex);
			}
		}

		public void Error(string msg, Exception ex)
		{
			log("ERROR", msg + ex.ToString() + "\n" + ex.StackTrace);
		}
		public void Error(string msg)
		{
			log("ERROR", msg);
		}

		public void Info(string msg)
		{
			log("INFO", msg);
		}
		public void Debug(string msg)
		{
			log("DEBUG", msg);
		}

		private void log(string level, string msg)
		{
			System.Diagnostics.Trace.TraceInformation("BetterReminders - "+level+" - "+msg);
			if (sw != null)
				sw.WriteLine("[" + Thread.CurrentThread.Name + " " + DateTime.Now + "] "+level+" - " + msg);
		}

		public void Shutdown()
		{
			sw.Close();
			sw = null;
		}
	}
}