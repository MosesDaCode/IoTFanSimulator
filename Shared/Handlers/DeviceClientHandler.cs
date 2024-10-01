﻿using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Shared.Models;
using System.Text;

namespace Shared.Handlers
{
	public class DeviceClientHandler
	{
        private readonly DeviceSettings _settings;
		public DeviceClient? Client {  get; private set; }

        public DeviceClientHandler(string deviceId, string deviceName, string deviceType)
        {
            _settings!.deviceId = deviceId;
            _settings.deviceName = deviceName;
            _settings.deviceType = deviceType;
        }

        public async Task Initialize()
        {
            Client = DeviceClient.CreateFromConnectionString(_settings.ConnectionString);
            Client.SetMethodDefaultHandlerAsync(DirectMethodDefaultCallback, null);
        }

        public Task<MethodResponse> DirectMethodDefaultCallback(MethodRequest request, object userContext)
        {

            var methodResponse = request.Name.ToLower() switch
            {
                "start" => OnStart(),
                "stop" => OnStop(),
                _ => GenerateMethodResponse("No suitable metohod found", 404)
			};

            return Task.FromResult(methodResponse);
        }

        public MethodResponse OnStart()
        {
            _settings.DeviceState = true;
            return GenerateMethodResponse("DeviceState changed set to start", 200);
        }

        public MethodResponse OnStop()
        {
            _settings.DeviceState = false;
			return GenerateMethodResponse("DeviceState changed set to stop", 200);
		}

        public MethodResponse GenerateMethodResponse(string message, int statusCode) 
        {
            try
            {
				var json = JsonConvert.SerializeObject(new { Message = message });
				var methodResponse = new MethodResponse(Encoding.UTF8.GetBytes(json), statusCode);
				return methodResponse;
			}
            catch
            {
				var json = JsonConvert.SerializeObject(new { Message = message });
				var methodResponse = new MethodResponse(Encoding.UTF8.GetBytes(json), statusCode);
				return methodResponse;
			}

        }
    }
}
