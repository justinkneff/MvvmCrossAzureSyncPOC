using Autofac;
using MvvmCrossSyncPOC.Core.AzureAbstractions;
using MvvmCrossSyncPOC.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvvmCrossSyncPOC.iOS.Modules
{
    public class ServiceClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder
                .RegisterType<AzureCloudService>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<ToDoItemRepositoryService>()
                .AsImplementedInterfaces()
                .SingleInstance();

        }
    }
}
