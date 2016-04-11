using System;
namespace co.chimeralabs.ads.managed.Utils
{

	public class UtilityMethods
	{
        public static long GetCurrentTimeStamp()
        {
            DateTime nx = new DateTime(1970, 1, 1); // UNIX epoch date
            TimeSpan ts = DateTime.UtcNow - nx; // UtcNow, because timestamp is in GMT
			return (long)ts.TotalSeconds;
        }
	}
}