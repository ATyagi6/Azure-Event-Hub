using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WiredBrainCoffee.EventHub.Sender;

namespace WiredBrainCoffee.MachineSimulator.UI.ViewModel
{
    public class MainViewModel : BindableBase

    {
        private int _counterCappuccino;
        private int _counterEspresso;
        private string _city;
        private string _serialNumber;
        private ICoffeeMachineDataSender _coffeeMachineDataSender;

        public MainViewModel(ICoffeeMachineDataSender coffeeMachineDataSender)
        {
            _serialNumber = Guid.NewGuid().ToString().Substring(0, 8);
            MakeCappuccinoCommand = new DelegateCommand(MakeCappuccino);
            MakeEspressoCommand = new DelegateCommand(MakeEspresso);
            _coffeeMachineDataSender = coffeeMachineDataSender;
        }

        public ICommand MakeCappuccinoCommand { get; }
        public ICommand MakeEspressoCommand { get; }

        public string SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                _serialNumber = value;
                RaisePropertyChanged();
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                RaisePropertyChanged();
            }
        }

        public int CounterCappuccino
        {
            get { return _counterCappuccino; }
            set
            {
                _counterCappuccino = value;
                RaisePropertyChanged();
            }
        }

        public int CounterEspresso
        {
            get { return _counterEspresso; }
            set
            {
                _counterEspresso = value;
                RaisePropertyChanged();
            }
        }
        private async void MakeCappuccino()
        {
            CounterCappuccino++;
            var coffeeMachineData = new CoffeeMachineData
            {
                City = City,
                SerialNumber = SerialNumber,
                SensorType = nameof(CounterCappuccino),
                SensorValue = CounterCappuccino,
                RecordingTime = DateTime.Now
            };
            await SendDataAsync(coffeeMachineData);
        }
        private async void MakeEspresso()
        {
            CounterEspresso++;
            var coffeeMachineData = new CoffeeMachineData
            {
                City = City,
                SerialNumber = SerialNumber,
                SensorType = nameof(CounterEspresso),
                SensorValue = CounterEspresso,
                RecordingTime = DateTime.Now
            };
           await SendDataAsync(coffeeMachineData);
        }

        private async Task SendDataAsync(CoffeeMachineData coffeeMachineData)
        {
            await _coffeeMachineDataSender.SendAsync(coffeeMachineData);
            
        }
    }
}