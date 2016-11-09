using System;
using System.IO;
using System.Web.Hosting;

// Create our own utility for exceptions 
namespace Hozpm.Logic
{
	public static class ExceptionUtility
	{
		public const string LogPath = "~/App_Data/Logs";
		public const string LogName = "Application_Error_{0}.txt";

		public static void LogException(Exception exc)
		{
			var logDir = HostingEnvironment.MapPath(LogPath);

			if (string.IsNullOrEmpty(logDir))
				throw new Exception("MapPath returend invalid directory.");

			if (!Directory.Exists(logDir))
				Directory.CreateDirectory(logDir);

			var logName = string.Format(LogName, DateTime.Today.ToString("dd-MM-yyyy"));
			var logPath = Path.Combine(logDir, logName);
			
			using (var sw = new StreamWriter(logPath, true))
			{
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
}