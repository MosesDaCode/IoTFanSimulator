using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class AppConfig
    {
        public static string HubConnectionString { get; set; } = "HostName=Moshe-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=6RXcZOjg0nyN2X7ydZ8UT0O5lKWjLYSpSAIoTEQoJs0=";
        public static string DeviceConnectionString { get; set; } = "HostName=Moshe-iothub.azure-devices.net;DeviceId=f3601a38-ba7d-49f5-a70c-71ac060dcfca;SharedAccessKey=siPEdCcf7h6X0grAYr18/nccCdZPH7UBj6fExZHacps=";
    }
}
