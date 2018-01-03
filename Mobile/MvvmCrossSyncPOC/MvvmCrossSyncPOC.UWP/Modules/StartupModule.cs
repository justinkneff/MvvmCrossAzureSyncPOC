using Autofac;
using MvvmCross.Core.ViewModels;
using MvvmCrossSyncPOC.Core.AzureAbstractions;
using MvvmCrossSyncPOC.Core.Services;
using MvvmCrossSyncPOC.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MvvmCrossSyncPOC.UWP.Modules
{
    public class StartupModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var viewModelAssembly = typeof(FirstViewModel)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(viewModelAssembly)
                .AssignableTo<MvxViewModel>()
                .As<IMvxViewModel, MvxViewModel>()
                .AsSelf();

            builder.RegisterType<MvxDefaultViewModelLocator>()
                .As<IMvxViewModelLocator>();

            builder.RegisterType<MvxDefaultViewModelLocator>()
                .As<IMvxViewModelLocator>();

            builder
                .RegisterType<AppSettings>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterModule<MobileServiceClientModule>();

            builder
                .RegisterType<ToDoItemRepositoryService>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<CloudServiceContainer>()
                .AsImplementedInterfaces()
                .SingleInstance();

        }
    }
}
