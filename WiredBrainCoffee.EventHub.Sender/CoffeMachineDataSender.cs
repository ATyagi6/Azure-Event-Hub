using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WiredBrainCoffee.EventHub.Sender
{
    public interface ICoffeeMachineDataSender
    {
         Task SendAsync(CoffeeMachineData data);
    }
  public   class CoffeMachineDataSender :ICoffeeMachineDataSender
    {
        private EventHubClient _eventHubClient;

        public CoffeMachineDataSender(string eventHubConnectionString)
        {
            _eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionString);
        }
        public async  Task SendAsync (CoffeeMachineData data)
        {
            
            var dataAsJson = JsonConvert.SerializeObject(data);
            var eventData = new EventData(Encoding.UTF8.GetBytes(dataAsJson));
            await  _eventHubClient.SendAsync(eventData);
        }

    }
}
