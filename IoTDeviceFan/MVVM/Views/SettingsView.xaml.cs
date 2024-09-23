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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IoTDeviceFan.MVVM.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
			DataContext = new SettingsViewModel();
        }

		private void TopWindowBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Window.GetWindow(this)?.DragMove();
		}
		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			var mainWindow = (MainWindow)Application.Current.MainWindow;

			mainWindow.ShowMainView();
		}
		

	}
}
