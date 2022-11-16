using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Runtime.CompilerServices;

namespace ETicaretAPI.API.Configurations.ColumnWriters
{
    public class UsernameColumnWriter : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UsernameColumnWriter(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var userName = _contextAccessor.HttpContext?.User.Identity.Name;
            string val = userName?.ToString() ?? null;
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("user_name", userName));

        }
    }
}
