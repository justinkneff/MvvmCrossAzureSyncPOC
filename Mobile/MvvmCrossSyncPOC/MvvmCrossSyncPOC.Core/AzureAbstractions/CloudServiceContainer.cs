using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace MvvmCrossSyncPOC.Core.AzureAbstractions
{
    public class CloudServiceContainer : ICloudServiceContainer
    {
        private readonly IComponentContext _componentContext;

        public CloudServiceContainer(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }
        public ICloudService GetCloudService()
        {
            return _componentContext.Resolve<ICloudService>();
        }
    }
}
