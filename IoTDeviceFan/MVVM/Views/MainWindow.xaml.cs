using IoTDeviceFan.MVVM.ViewModels;
using IoTDeviceFan.MVVM.Views;
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

namespace IoTDeviceFan.MVVM.Views
{
    public partial class MainWindow : Window
	{
		
		public FanViewModel ViewModel {  get; set; }
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new FanViewModel();

			MainContentControl.Content = new MainView();
		}

		public void ShowSettingsView()
		{
			MainContentControl.Content = new SettingsView();
		}
		public void ShowMainView()
		{
			MainContentControl.Content = new MainView();
		}
	}
}