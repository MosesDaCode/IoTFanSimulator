using Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDeviceFan.MVVM.ViewModels
{
    public class MainViewModel
    {
        private readonly IoTHubService? _hubService;

        public MainViewModel()
        {
            _hubService = new IoTHubService("HostName=Moshe-iothub.azure-devices.net;DeviceId=3a1eced5-476b-4b26-bf25-6a7179f0ea02;SharedAccessKey=xjoIN3LXJWO7q1IV77WfKVKZwVMisufQcAIoTK8p0Xk=");
        }

        public async Task SendStatusMessage()
        {
            string message = "{ \"status\": \"on\" }";

            await _hubService.SendMessageAsync(message);
        }
    }
}
