using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace LocalhostAPI.Behaviors
{
    public class EnableCorsEndpointBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        public override Type BehaviorType
        {
            get
            {
                return typeof(EnableCorsEndpointBehavior);
            }
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            var headers = new Dictionary<string, string>
            {
                {"Access-Control-Origin", "*" },
                {"Access-Control-Request-Method", "POST,GET,PUT,DELETE,OPTIONS"},
                {"Access-Control-ALlow-Headers", "X-Requested-With,Content-Type" }
            };

            var inspector = new CustomHeaderMessageInspector(headers);
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
        }

        public void Validate(ServiceEndpoint endpoint)
        { }

        protected override object CreateBehavior()
        {
            return new EnableCorsEndpointBehavior();
        }
    }
}
