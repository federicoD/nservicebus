
using System.Configuration;
using Shared;

namespace ReceiverA
{
    using NServiceBus;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint, IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.Pipeline.Register<RegisterOriginalSenderCheckStep>();
            configuration.EndpointName(ConfigurationManager.AppSettings["IntegrationPointName"]);
        }

        public void Start()
        {
            Bus.Subscribe<DataProcessedEvent>();
        }

        public void Stop()
        {
        }
    }
}
