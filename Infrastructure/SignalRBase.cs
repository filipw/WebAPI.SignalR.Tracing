using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.SignalR.Tracing.Infrastructure
{
    public abstract class SignalRBase<T> where T : IHub
    {
        private Lazy<IHubContext> hub = new Lazy<IHubContext>(
            () => GlobalHost.ConnectionManager.GetHubContext<T>()
        );

        protected IHubContext Hub
        {
            get { return hub.Value; }
        }
    }
}