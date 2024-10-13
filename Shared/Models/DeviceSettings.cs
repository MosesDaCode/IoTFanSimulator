namespace Shared.Models
{
	public class DeviceSettings
	{
		public string DeviceId { get; set; } = null!;
 		public string? DeviceName { get; set; }
		public string? DeviceType { get; set; }
		public bool DeviceState { get; set; }
			
		public string ConnectionString { get; set; } = null!;
		public bool ConnectionState { get; set; }

		public string? Email { get; set; }
	}

}
