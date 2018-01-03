using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvvmCrossSyncPOC.UWP.Modules
{
    public class AutofacBootstrapper
    {
        public static IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<Modules.StartupModule>();

            return builder.Build();
        }
    }
}
