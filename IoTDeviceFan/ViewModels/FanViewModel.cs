using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDeviceFan.ViewModels
{
    public class FanViewModel : INotifyPropertyChanged
    {
		private bool _isRunning;
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
	}
}
