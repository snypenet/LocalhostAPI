using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace LocalhostAPI.Behaviors
{
    internal class CustomHeaderMessageInspector : IDispatchMessageInspector
    {
        private readonly Dictionary<string, string> _Headers;
        public CustomHeaderMessageInspector(Dictionary<string, string> headers)
        {
            _Headers = headers;
        }
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var httpHeader = reply.Properties["httpResponse"] as HttpRequestMessageProperty;
            if(httpHeader != null)
            {
                foreach(var item in _Headers)
                {
                    httpHeader.Headers.Add(item.Key, item.Value);
                }
            }
        }
    }
}
