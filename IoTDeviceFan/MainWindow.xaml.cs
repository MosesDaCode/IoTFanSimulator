using IoTDeviceFan.MVVM.ViewModels;
using System.Text;
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

namespace IoTDeviceFan
{
    public partial class MainWindow : Window
	{
		public FanViewModel ViewModel {  get; set; }
		public MainWindow()
		{
			InitializeComponent();
			ViewModel = new FanViewModel();
			DataContext = ViewModel;
		}

		private void TopWindowBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
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

		


	}
}