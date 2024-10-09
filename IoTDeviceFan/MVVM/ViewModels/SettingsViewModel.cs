using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Handlers;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IoTDeviceFan.MVVM.ViewModels
{
	public class SettingsViewModel : ObservableObject
	{

		private string? _connectionString;
		private string? _deviceId;
		private DeviceClientHandler _client;
		 
		public string ConnectionString
		{
			get => _connectionString;
			set => SetProperty(ref _connectionString, value);
		}

		public string DeviceId
		{
			get => _deviceId;
			set => SetProperty(ref _deviceId, value);
		}

		public ICommand SaveCommand { get; }
		public ICommand InitializeDeviceCommand { get; }

		public SettingsViewModel()
		{
			LoadSettings();
			SaveCommand = new RelayCommand(SaveSettings);
			InitializeDeviceCommand = new RelayCommand(async () => await InitializeDeviceAsync());


			if (!string.IsNullOrEmpty(DeviceId))
			{
                _client = new DeviceClientHandler(DeviceId, "Fan", "Fan", AppConfig.HubConnectionString);
            }
			else
			{
				Console.WriteLine("DeviceId is not set. Please check your settings");
			}
        }

		private async Task InitializeDeviceAsync()
		{
			var result = await _client.InitializeAsync();
			Console.Write(result.Message);
			await LoadDeviceTwinAsync();
		}

		private async Task LoadDeviceTwinAsync()
		{
			var result = await _client.UpdateDeviceTwinDeviceStateAsync();
			if (result.Succeeded)
			{
				Console.WriteLine("Device twin properties updated");
			}
			else
			{
                Console.WriteLine($"Failed to update device twin properties: {result.Message}");

            }
        }


		private void SaveSettings()
		{
			Properties.Settings.Default.ConnectionString = ConnectionString;
			Properties.Settings.Default.DeviceId = DeviceId;
			Properties.Settings.Default.Save();
		}

		public void LoadSettings()
		{
			ConnectionString = Properties.Settings.Default.ConnectionString ?? string.Empty;
			DeviceId = Properties.Settings.Default.DeviceId ?? string.Empty;
		}
	}
}
