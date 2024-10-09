﻿using Microsoft.Azure.Devices;
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
        var connectionString = AppConfig.ConnectionString;
        _registry = RegistryManager.CreateFromConnectionString(connectionString);
        _serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
    }

     public async Task<IEnumerable<DeviceSettings>> GetDevicesAsync()
    {
        var query = _registry!.CreateQuery("select * from devices");
        var devices = new List<DeviceSettings>();

        foreach (var twin in await query.GetNextAsTwinAsync())
        {
            var device = new DeviceSettings
            {
                DeviceId = twin.DeviceId,
                DeviceName = twin.Properties.Reported["deviceName"]?.ToString() ?? "",
                DeviceType = twin.Properties.Reported["deviceType"]?.ToString() ?? ""
            };

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

            devices.Add(device);
        }
        return devices;
    }

    public async Task SendDirectMethodAsync(string deviceId, string methodName)
    {
        var methodInvocation = new CloudToDeviceMethod(methodName) { ResponseTimeout = TimeSpan.FromSeconds(10)};
        var response = await _serviceClient!.InvokeDeviceMethodAsync(deviceId, methodInvocation);


    }
}

