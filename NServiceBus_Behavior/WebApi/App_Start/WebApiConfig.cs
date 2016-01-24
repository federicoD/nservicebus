using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Autofac;
using NServiceBus;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static IBus MyBus;

        public static void Register(HttpConfiguration config)
        {
            BusConfiguration configuration = new BusConfiguration();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.EndpointName(ConfigurationManager.AppSettings["EndPoint"]);
            configuration.EnableInstallers();

            // Web API configuration and services

            MyBus = Bus.Create(configuration).Start();

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
