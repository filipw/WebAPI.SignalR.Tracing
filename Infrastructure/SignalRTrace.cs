using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Tracing;
using WebApi.SignalR.Tracing.Hubs;
using WebApi.SignalR.Tracing.Extensions;

namespace WebApi.SignalR.Tracing.Infrastructure
{
    public class DynamicTrace : SignalRBase<TraceHub>, ITraceWriter
    {
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level != TraceLevel.Off)
            {
                TraceRecord record = new TraceRecord(request, category, level);
                traceAction(record);
                Log(record);
            }
        }

        private void Log(TraceRecord record)
        {
            var message = new StringBuilder();

            message.AppendMessage(record.Level.ToString().ToUpper());
            message.AppendMessage(DateTime.Now.ToString());

            if (record.Request != null)
                message.AppendMessage(record.Request.Method.ToString(), notEmpty).AppendMessage(record.Request.RequestUri.ToString(), notEmpty);

            message.AppendMessage(record.Category, notEmpty).AppendMessage(record.Operator, notEmpty)
                .AppendMessage(record.Operation).AppendMessage(record.Message, notEmpty);

            if (record.Exception != null)
                message.AppendMessage(record.Exception.GetBaseException().Message, notEmpty);

            Hub.Clients.All.logMessage(message.ToString());
        }

        Func<string, bool> notEmpty = (text) => !string.IsNullOrWhiteSpace(text);
    }
}