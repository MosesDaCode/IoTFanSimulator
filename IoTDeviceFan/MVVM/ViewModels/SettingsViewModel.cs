using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

		public SettingsViewModel()
		{
			LoadSettings();
			SaveCommand = new RelayCommand(SaveSettings);
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
