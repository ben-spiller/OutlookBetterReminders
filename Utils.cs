using System;
using System.IO;
using System.Threading;
namespace BetterReminders
{
	class Logger
	{
		// make it static so can be shared by multiple classes
		static StreamWriter sw = new StreamWriter(new FileStream(@"C:\dev\vs2013\BetterReminders/outlook-ben.log", FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
		{
			AutoFlush = true
		};
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
			sw.WriteLine("[" + Thread.CurrentThread.Name + " " + DateTime.Now + "] "+level+" - " + msg);
		}

		public void Shutdown()
		{
			sw.Close();
		}
	}
}