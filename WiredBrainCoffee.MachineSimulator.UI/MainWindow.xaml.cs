using System.Configuration;
using System.Windows;
using WiredBrainCoffee.EventHub.Sender;
using WiredBrainCoffee.MachineSimulator.UI.ViewModel;

namespace WiredBrainCoffee.MachineSimulator.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var eventHUbConnectionString = ConfigurationManager.AppSettings["EventHubConnectionString"];
            DataContext = new MainViewModel(new CoffeMachineDataSender(eventHUbConnectionString));
        }
    }
}
