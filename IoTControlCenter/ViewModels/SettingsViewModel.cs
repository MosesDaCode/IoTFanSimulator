using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IoTControlCenter.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private string? _connectionString;
        private string? _email;


        public string ConnectionString
        {
            get => _connectionString;
            set => SetProperty(ref _connectionString, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public ICommand SaveCommand { get; }

        public SettingsViewModel()
        {
            SaveCommand = new RelayCommand(SaveSettings);
            LoadSettings();
        }


        public void SaveSettings()
        {
            var settings = new AppSettings
            {
                ConnectionString = ConnectionString,
                Email = Email
            };

            DataManager.SaveSettings(settings);
        }

        private void LoadSettings()
        {
            var settings = DataManager.LoadSettings();
            ConnectionString = settings.ConnectionString;
            Email = settings.Email;
        }
    }
}
