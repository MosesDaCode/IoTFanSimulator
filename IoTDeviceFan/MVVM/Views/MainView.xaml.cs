using IoTDeviceFan.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IoTDeviceFan.MVVM.Views
{
    public partial class MainView : UserControl
    {
        private MainView _mainWiew;
        private SettingsView _settingsWiew;
		public FanViewModel ViewModel { get; set; }
		public MainView()
		{
			InitializeComponent();
			ViewModel = new FanViewModel();
			DataContext = ViewModel;

		}

		private void TopWindowBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Window window = Window.GetWindow(this);
			if (window != null)
				window.DragMove();
		}
		private async void StartButton_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.ToggleDevice();

			var sb = (BeginStoryboard)TryFindResource("rotate-sb");

			if (ViewModel.IsRunning)
			{
				sb.Storyboard.Begin();
			}
			else
			{
				sb.Storyboard.Stop();
			}

			string message = ViewModel.IsRunning ? "{ \"status\": \"on\" }" : "{ \"status\": \"off\" }";

			await ViewModel.SendStatusMessage(message);
		}
		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		private void SettingsButton_Click(object sender, RoutedEventArgs e)
		{
			var mainWindow = (MainWindow)Application.Current.MainWindow;
			mainWindow.ShowSettingsView();
		}
	}
}
