using Shared.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDeviceFan.MVVM.ViewModels
{
    public class FanViewModel : INotifyPropertyChanged
    {
        private readonly IoTHubService _hubService;
        private bool _isRunning;

        public FanViewModel() => _hubService = new IoTHubService("HostName=Moshe-iothub.azure-devices.net;DeviceId=3a1eced5-476b-4b26-bf25-6a7179f0ea02;SharedAccessKey=xjoIN3LXJWO7q1IV77WfKVKZwVMisufQcAIoTK8p0Xk=");

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        public string ButtonText => IsRunning ? "STOP" : "START";


        public void ToggleDevice()
        {
            IsRunning = !IsRunning;
        }

        public void SaveSettings(string connectionString, string deviceId)
        {
            Properties.Settings.Default.ConnectionString = connectionString;
            Properties.Settings.Default.DeviceId = deviceId;
            Properties.Settings.Default.Save();
        }
        public void LoadSettings()
        {
            string connectionString = Properties.Settings.Default.ConnectionString;
            string deviceId = Properties.Settings.Default.DeviceId;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task SendStatusMessage(string message)
        {
            await _hubService.SendMessageAsync(message);
        }
    }
}
