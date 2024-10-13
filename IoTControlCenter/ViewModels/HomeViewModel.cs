using Shared.Data;
using Shared.Handlers;
using Shared.Models;
using Shared.Services;
using System.Diagnostics;

namespace IoTControlCenter.ViewModels
{
    public class HomeViewModel
    {
        private readonly IotHubHandler _iotHub;
        private readonly EmailService _emailService;
        public Timer? Timer { get; set; }
        public int TimeInterval { get; set; } = 4000;

        public HomeViewModel(IotHubHandler iotHub, EmailService emailService)
        {
            _iotHub = iotHub;
            _emailService = emailService;

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
            await _iotHub.SendDirectMethodAsync(device.DeviceId, methodName);
        }

        public async Task ToggleDeviceAsync(DeviceSettings device)
        {

            if (device.ConnectionState)
            {
                await _iotHub.SendDirectMethodAsync(device.DeviceId, "Disconnect");
                device.DeviceState = false;
            }
            else
            {
                await _iotHub.SendDirectMethodAsync(device.DeviceId, "Connect");
                device.ConnectionState = true;
            }
        }

        public async Task DeleteDeviceAsync(DeviceSettings device)
        {

            try
            {
                var settings = DataManager.LoadSettings();

                if (!string.IsNullOrEmpty(settings.Email))
                {

                    await _iotHub.DeleteDeviceAsync(device.DeviceId);

                    await _emailService.SendEmailAsync(settings.Email, "Device Removed", $"The device {device.DeviceName} has been removed.");
                }
                else
                {
                    Debug.WriteLine("Email address was not found in settings");
                }
                await GetDevicesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }
}
