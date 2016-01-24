using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using Shared;

namespace ReceiverA
{
    public class DataProcessedEventHandler : IHandleMessages<DataProcessedEvent>
    {
        public void Handle(DataProcessedEvent message)
        {
            Console.WriteLine("[ReceiverA] Data has been processed. {0}", message.Result);
        }
    }

}
