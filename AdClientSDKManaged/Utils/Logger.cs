using System;
namespace co.chimeralabs.ads.managed.Utils
{

	public class Logger
	{
		public static System.IO.StreamWriter file;
		public static Boolean isDebug = false;
		public static void Log(Object context, String lines){
			if (!isDebug)
				return;
			file.Write (DateTime.Today);
			file.Write ("\t(");
			Type type = context.GetType();
			file.Write (type.FullName);
			file.Write (")\t");
			file.WriteLine (lines);
		}
		public static void Open(String filepath){
			file = new System.IO.StreamWriter ("test.txt", true);
		}
		public static void Close(){
			file.Close ();
		}
		public static void EnableLogging(){
			isDebug = true;
		}
		public static void DisableLogging(){
			isDebug = false;
		}

	}
}