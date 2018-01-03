using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace MvvmCrossSyncPOC.Core.AzureAbstractions
{
    public interface ICloudService
    {

        Task<IMobileServiceClient> GetMobileServiceClientAsync();

        Task SyncOfflineCacheAsync();

        Task ReinitializeTables();
    }
}
