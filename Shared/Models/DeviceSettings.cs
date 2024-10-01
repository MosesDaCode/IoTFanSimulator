namespace Shared.Models
{
	public class DeviceSettings
	{
		public string deviceId { get; set; } = null!;
 		public string? deviceName { get; set; }
		public string? deviceType { get; set; }
		public bool DeviceState { get; set; }

		public string ConnectionString { get; set; } = null!;
		public bool ConnectionState { get; set; }
	}

}
