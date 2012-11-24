using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.SignalR.Tracing.Hubs
{
    [HubName("trace")]
    public class TraceHub : Hub
    {}
}