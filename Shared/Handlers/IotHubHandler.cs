using Microsoft.Azure.Devices;
using Shared.Models;
using Shared.Services;
using System.Reflection;

namespace Shared.Handlers;

public class IotHubHandler
{
    private readonly RegistryManager? _registry;
    private readonly ServiceClient? _serviceClient;

    public IotHubHandler()
    {
        _registry = RegistryManager.CreateFromConnectionString(AppConfig.HubConnectionString);
        _serviceClient = ServiceClient.CreateFromConnectionString(AppConfig.HubConnectionString);
    }

    public async Task<IEnumerable<DeviceSettings>> GetDevicesAsync()
    {

        var devices = new List<DeviceSettings>();
        try
        {
            var query = _registry!.CreateQuery("select * from devices");


            foreach (var twin in await query.GetNextAsTwinAsync())
            {
                

                var device = new DeviceSettings
                {
                    DeviceId = twin.DeviceId
                };

                try
                {
                    device.DeviceName = twin.Properties.Reported["deviceName"]?.ToString() ?? "";
                }
                catch (Exception ex) { }

                try
                {
                    device.DeviceType = twin.Properties.Reported["deviceType"]?.ToString() ?? "";
                }
                catch (Exception ex) { }

                try
                {
                    bool.TryParse(twin.Properties.Reported["connectionState"].ToString(), out bool connectionState);
                    device.ConnectionState = connectionState;

                    if (device.ConnectionState)
                    {
                        bool.TryParse(twin.Properties.Reported["deviceState"].ToString(), out bool deviceState);
                        device.DeviceState = deviceState;
                    }
                    else
                    {
                        device.DeviceState = false;
                    }
                }
                catch { }

                devices.Add(device);
            }
        }
        catch (Exception ex)
        {

        }
        return devices;
    }

    public async Task SendDirectMethodAsync(string deviceId, string methodName)
    {
        var methodInvocation = new CloudToDeviceMethod(methodName) { ResponseTimeout = TimeSpan.FromSeconds(10)};
        var response = await _serviceClient!.InvokeDeviceMethodAsync(deviceId, methodInvocation);


    }
}

