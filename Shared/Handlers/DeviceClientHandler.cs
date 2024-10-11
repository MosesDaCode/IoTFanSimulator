using Azure;
using DotNetty.Codecs.Mqtt.Packets;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using Shared.Models;
using Shared.Services;
using System.Diagnostics;
using System.Text;

namespace Shared.Handlers
{
    public class DeviceClientHandler
	{
        private readonly DeviceSettings _settings;
		private DeviceClient? _client;

        public DeviceClientHandler(string deviceId, string deviceName, string deviceType, string connectionString, bool connectionState, bool deviceState)
        {
            _settings = new DeviceSettings
            {
                DeviceId = deviceId,
                DeviceName = deviceName,
                DeviceType = deviceType,
                ConnectionString = connectionString,
                ConnectionState = connectionState,
                DeviceState = deviceState
                
            };
        }

        public async Task<ResultResponse> InitializeAsync()
        {
            var response = new ResultResponse();
            try
            {
                _client = DeviceClient.CreateFromConnectionString(_settings.ConnectionString);
                if (_client != null)
                {
                    await _client.SetMethodDefaultHandlerAsync(DirectMethodDefaultCallback, null);
                    await UpdateDeviceTwinPropertiesAsync();

                    response.Succeeded = true;
                    response.Message = "Device Initialized!";
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "Device client was not found";
                }
            }
            catch (Exception ex){
                response.Succeeded = false;
                response.Message = ex.Message;
            }
            return response; 
        }

        public async Task<MethodResponse> DirectMethodDefaultCallback(MethodRequest request, object userContext)
        {

            var methodResponse = request.Name.ToLower() switch
            {
                "start" => await OnStartAsync(),
                "stop" => await OnStopAsync(),
                _ => GenerateMethodResponse("No suitable metohod found", 404)
			};

            return methodResponse;
        }

        public async Task<MethodResponse> OnStartAsync()
        {
            _settings.DeviceState = true;

            var result = await UpdateDeviceTwinDeviceStateAsync();
            if (result.Succeeded)
                return GenerateMethodResponse("DeviceState changed set to start", 200);
            else
                return GenerateMethodResponse($"{result.Message}", 400);
        }

        public async Task<MethodResponse> OnStopAsync()
        {
            _settings.DeviceState = false;

            var result = await UpdateDeviceTwinDeviceStateAsync();
            if (result.Succeeded)
                return GenerateMethodResponse("DeviceState changed set to stop", 200);
            else
                return GenerateMethodResponse($"{result.Message}", 400);
            
		}

        public MethodResponse GenerateMethodResponse(string message, int statusCode) 
        {
            try
            {
				var json = JsonConvert.SerializeObject(new { Message = message });
				var methodResponse = new MethodResponse(Encoding.UTF8.GetBytes(json), statusCode);
				return methodResponse;
			}
            catch (Exception ex)
            {
				var json = JsonConvert.SerializeObject(new { Message = ex.Message });
				var methodResponse = new MethodResponse(Encoding.UTF8.GetBytes(json), statusCode);
				return methodResponse;
			}

        }

        public async Task <ResultResponse> UpdateDeviceTwinDeviceStateAsync()
        {
            var response = new ResultResponse();
            try
            {
                var reportedProperties = new TwinCollection
                {
                    ["deviceState"] = _settings.DeviceState
                };
                
                if (_client != null)
                {
                    await _client!.UpdateReportedPropertiesAsync(reportedProperties);
                    response.Succeeded = true;
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "Device client was not found";
                }
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = ex.Message;
            }
            return response;

        }
        public async Task <ResultResponse> UpdateDeviceTwinPropertiesAsync()
        {
            var response = new ResultResponse();
            try
            {
                var reportedProperties = new TwinCollection
                {

                    ["connectionState"] = _settings.ConnectionState,
                    ["deviceName"] = _settings.DeviceName,
                    ["deviceType"] = _settings.DeviceType,
                    ["deviceState"] = _settings.DeviceState
                };
                
                if (_client != null)
                {
                    await _client!.UpdateReportedPropertiesAsync(reportedProperties);
                    response.Succeeded = true;
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "Device client was not found";
                }
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = ex.Message;
            }
            return response;

        }


        public async Task<ResultResponse> ConnectAsync()
        {
            _settings.ConnectionState = true;
            var result = await UpdateDeviceTwinDeviceStateAsync();
            return result.Succeeded ? new ResultResponse { Succeeded = true, Message = "Device connected" }
                                    : new ResultResponse { Succeeded = false, Message = "Failed to connect" };
        }
        
        public async Task<ResultResponse> DisconnectAsync()
        {
            _settings.ConnectionState = false;
            var result = await UpdateDeviceTwinDeviceStateAsync();
            return result.Succeeded ? new ResultResponse { Succeeded = true, Message = "Device disconnected" }
                                    : new ResultResponse { Succeeded = false, Message = "Failed to disconnect" };
        }
    }
}
