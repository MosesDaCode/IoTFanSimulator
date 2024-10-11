using CommunityToolkit.Mvvm.ComponentModel;
using Shared.Handlers;
using Shared.Models;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDeviceFan.MVVM.ViewModels
{
    public class FanViewModel : ObservableObject
    {

        private readonly DeviceClientHandler _client;
        private readonly HubService _hubService;
        private bool _isRunning;

        public FanViewModel()
        {
            _client = new DeviceClientHandler("DeviceId", "Fan", "Fan", AppConfig.DeviceConnectionString, true, true);
            _hubService = new HubService(AppConfig.DeviceConnectionString);
            InitializeClientAsync();
        }

        private async void InitializeClientAsync()
        {
            var result = await _client.InitializeAsync();
            if (result.Succeeded)
                Debug.WriteLine("Device  connected successfully!");
            else
                Debug.WriteLine($"Failed to connect device: {result.Message}");
        }

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


        public async void ToggleDevice()
        {

            var result = await _client!.UpdateDeviceTwinPropertiesAsync();

            if (result.Succeeded)
            {
                Debug.WriteLine(result.Message);
            }
            else
            {
                Debug.WriteLine(result.Message);
            }

            string message = IsRunning ? "{ \"status\": \"on\" }" : "{ \"status\": \"off\" }";

            await SendStatusMessageAsync(message);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task SendStatusMessageAsync(string message)
        {
            await _hubService.SendMessageAsync(message);
        }
    }
}
