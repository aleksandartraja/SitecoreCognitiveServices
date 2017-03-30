﻿using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.SharedSource.CognitiveServices.Foundation;

namespace Sitecore.SharedSource.CognitiveServices.LaunchDemo.Configurator
{
    public class CognitiveLaunchConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcControllersInCurrentAssembly();
        }
    }
}