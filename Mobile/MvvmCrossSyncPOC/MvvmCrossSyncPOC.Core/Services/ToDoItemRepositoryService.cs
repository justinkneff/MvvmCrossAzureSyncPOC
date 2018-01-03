using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCrossSyncPOC.Core.AzureAbstractions;
using Microsoft.WindowsAzure.MobileServices;

namespace MvvmCrossSyncPOC.Core.Services
{
    public class ToDoItemRepositoryService : BaseSyncRepository<ToDoItem>, IToDoItemRepositoryService
    {
        public ToDoItemRepositoryService(ICloudServiceContainer mobileServiceClient) : base(mobileServiceClient)
        {
        }

        public override async Task PullAsync()
        {
            var queryName = $"incsync:r::{typeof(ToDoItem).Name}";
            var query = Table.Value.CreateQuery();

            await Table.Value.PullAsync(queryName, query);
        }

        public async Task ReinitializeTable()
        {
            await base.InitializeTable();
        }
    }

    public interface IToDoItemRepositoryService
    {
        Task PullAsync();
        Task ReinitializeTable();
    }
}
