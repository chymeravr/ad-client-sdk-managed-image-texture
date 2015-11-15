using System;
using System.Collections.Generic;
using System.IO;

namespace co.chimeralabs.ads.managed.Utils
{
	public class PropertyFileReader
	{
		public static Dictionary<String, String> LoadProperties(String filePath){
			Dictionary<String, String> data = new Dictionary<String, String>();
			foreach (String row in File.ReadAllLines(filePath)) {
				String key = row.Split ('=') [0];
				data.Add (key, row.Substring(key.Length)); //string.Join ("=", row.Split ('=').Skip (1).ToArray ()));
			}
			return data;
		}
	}
}
