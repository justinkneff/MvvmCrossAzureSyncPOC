using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MvvmCrossSyncPOC.Core.AzureAbstractions;
using MvvmCrossSyncPOC.Core.Services;

namespace MvvmCrossSyncPOC.iOS.Modules
{
    public class MobileServiceClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            SQLitePCL.Batteries.Init();

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
