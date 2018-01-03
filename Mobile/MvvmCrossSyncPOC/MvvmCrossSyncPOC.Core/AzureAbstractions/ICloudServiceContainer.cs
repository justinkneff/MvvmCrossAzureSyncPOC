using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCrossSyncPOC.Core.AzureAbstractions
{
    public interface ICloudServiceContainer
    {
        ICloudService GetCloudService();
    }
}
