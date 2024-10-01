using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data
{
	public class DataManager
	{
		private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "AppSettings.json");
		public static void SaveSettings(AppSettings settings)
		{
			string json = JsonConvert.SerializeObject(settings, Formatting.Indented);

			Directory.CreateDirectory(Path.GetDirectoryName(filePath));

			File.WriteAllText(filePath, json);
		}

		public static AppSettings LoadSettings()
		{
			if (File.Exists(filePath))
			{
				string json = File.ReadAllText(filePath);
				return JsonConvert.DeserializeObject<AppSettings>(json);
			}
			return new AppSettings();
		}
	}
}
