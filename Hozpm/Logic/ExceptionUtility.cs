using System;
using System.IO;
using System.Web;

// Create our own utility for exceptions 
namespace Hozpm.Logic
{
	public static class ExceptionUtility
	{
		public const string LogPath = "App_Data/Application_Error.txt";

		// Log an Exception 
		public static void LogException(Exception exc)
		{
			// Include enterprise logic for logging exceptions 
			// Get the absolute path to the log file 
			var logFile = HttpContext.Current.Server.MapPath(LogPath);

			// Open the log file for append and write the log
			var sw = new StreamWriter(logFile, true);
			sw.WriteLine("********** {0} **********", DateTime.Now);

			sw.WriteLine($"Exception Type: {exc.GetType()}");
			sw.WriteLine($"Message: {exc.Message}");
			sw.WriteLine($"Source: {exc.Source}");
			if (exc.StackTrace != null)
			{
				sw.WriteLine("Stack Trace: ");
				sw.WriteLine(exc.StackTrace);
				sw.WriteLine();
			}

			if (exc.InnerException != null)
			{
				sw.WriteLine($"Inner Exception Type: {exc.InnerException.GetType()}");
				sw.WriteLine($"Message: {exc.InnerException.Message}");
				sw.WriteLine($"Source: {exc.InnerException.Source}");
				if (exc.InnerException.StackTrace != null)
				{
					sw.WriteLine("Inner Stack Trace: ");
					sw.WriteLine(exc.InnerException.StackTrace);
					sw.WriteLine();
				}
			}
			
			sw.Close();
		}
	}
}