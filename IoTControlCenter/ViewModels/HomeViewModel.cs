using Shared.Handlers;
using Shared.Models;

namespace IoTControlCenter.ViewModels
{
    public class HomeViewModel
    {
        private readonly IotHubHandler _iotHub;

        public Timer? Timer { get; set; }
        public int TimeInterval { get; set; } = 4000;
        public HomeViewModel(IotHubHandler iotHub)
        {
            _iotHub = iotHub;
        }

        public async Task<IEnumerable<DeviceSettings>> GetDevicesAsync()
        {
            return await _iotHub.GetDevicesAsync();
        }
        public async Task OnDeviceStateChanged(DeviceSettings device)
        {
            Timer?.Change(Timeout.Infinite, Timeout.Infinite);
            await SendDirectMethodAsync(device);
            Timer?.Change(TimeInterval, TimeInterval);
        }

        public async Task SendDirectMethodAsync(DeviceSettings device)
        {
            var methodName = device.DeviceState ? "stop" : "start";
            await _iotHub.SendDirectMethodAsync(device.deviceId, methodName);
        }
    }
}
