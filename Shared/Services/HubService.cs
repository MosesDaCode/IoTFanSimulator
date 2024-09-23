using Microsoft.Azure.Devices.Client;
using System.Text;

namespace Shared.Services
{
    public class HubService
    {
        
        private DeviceClient _deviceClient;

        public HubService(string connectionString)
        {
            _deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);
        }


        public async Task SendMessageAsync(string content)
        {
            using var message = new Message(Encoding.UTF8.GetBytes(content))
            {
                ContentType = "application/json",
                ContentEncoding = "utf-8",
                CreationTimeUtc = DateTime.Now
            };

            await _deviceClient.SendEventAsync(message);
        }
    }
}