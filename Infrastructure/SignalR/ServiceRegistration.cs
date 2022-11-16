﻿using ETicaretAPI.Application.Abstraction.Hubs;
using ETicaretAPI.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddSignalR();
            collection.AddTransient<IProductHubService,ProductHubService>();
        }
    }
}