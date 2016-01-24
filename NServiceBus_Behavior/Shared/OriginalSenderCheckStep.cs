using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus.Pipeline;
using NServiceBus.Pipeline.Contexts;

namespace Shared
{
    public class OriginalSenderCheckStep : IBehavior<IncomingContext>
    {
        public void Invoke(IncomingContext context, Action next)
        {
            Dictionary<String, String> headers = context.PhysicalMessage.Headers;

            if (headers.ContainsKey(CustomHeaders.GeneratedBy))
            {
                String generatedBy = headers[CustomHeaders.GeneratedBy];

                if (String.Compare(
                    generatedBy,
                    ConfigurationManager.AppSettings["IntegrationPointName"],
                    StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return;
                }
            }

            next();
        }
    }

    public class RegisterOriginalSenderCheckStep : RegisterStep
    {
        public RegisterOriginalSenderCheckStep()
            : base("OriginalSenderCheckStep", typeof(OriginalSenderCheckStep), "Check if a specific header is found in the message")
        {
            base.InsertBefore(WellKnownStep.MutateIncomingTransportMessage);
        }
    }
}
