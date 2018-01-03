using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MvvmCrossSyncPOC.Core.AzureAbstractions;

namespace MvvmCrossSyncPOC.Core.Services
{
    public abstract class BaseSyncRepository<T> : IRepository<T> where T : class
    {
        private readonly ICloudServiceContainer _mobileServiceClient;
        private IMobileServiceClient client;
        /// <summary>
        /// Protected table allows for anyone who impelments the abstract class has access to use it so they can query faster and explicitly for objects.
        /// </summary>
        protected Lazy<IMobileServiceSyncTable<T>> Table;
        /// <summary>
        /// Base constructor takes a singleton of the mobile service client.
        /// </summary>
        /// <param name="mobileServiceClient"></param>
        protected BaseSyncRepository(ICloudServiceContainer mobileServiceClient)
        {
            _mobileServiceClient = mobileServiceClient;
            //NOTE : This is using a normal SQLLite table while 'offline'. This wont sync externally on each call (like .GetTable<T>).
            //          when using online individual sync calls for pull are required, and one universal push.
            InitializeTable().Wait();
        }
        /// <summary>
        /// Table Insert single item. ID must not be set.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task InsertItemAsync(T item)
        {
            await Table.Value.InsertAsync(item);
        }
        /// <summary>
        /// Get all items of registered type.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetItemsAsync()
        {
            return await Table.Value.ToEnumerableAsync();
        }
        /// <summary>
        /// Read item by ID. If any formatting is in the ID, they are removed. The GUID ID is removed by the Azure Client.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> ReadItemAsync(Guid id)
        {
            string sID = id.ToString("N");
            var obj = await Table.Value.LookupAsync(sID);
            // Added in this null check to see if some results are coming back with Guid formatting.
            if (obj == null)
            {
                obj = await Table.Value.LookupAsync(id.ToString());
            }
            return obj;
        }
        /// <summary>
        /// Update a single item in the respective typed tables only if the ID is set.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<T> UpdateItemAsync(T item)
        {
            await Table.Value.UpdateAsync(item);
            return item;
        }
        /// <summary>
        /// Delete an item from the tables.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task DeleteItemAsync(T item)
        {
            await Table.Value.DeleteAsync(item);
        }
        /// <summary>
        /// Insert Multiple Items
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task InsertItemsAsync(IEnumerable<T> items)
        {
            foreach (T obj in items)
            {
                await Table.Value.InsertAsync(obj);
            }
        }
        /// <summary>
        /// Sync to be used to push and pull all items
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<T>> ReadAllItemsAsync()
        {
            var items = (await Table.Value.ReadAsync()).ToList();
            return (ICollection<T>)items;
        }
        /// <summary>
        /// To be used for paging.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public Task<ICollection<T>> ReadItemsAsync(int start, int count)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// "pull data from the server" operation
        /// </summary>
        /// <returns></returns>
        public abstract Task PullAsync();
        

        public Task InitializeTable()
        {
            Table = new Lazy<IMobileServiceSyncTable<T>>(() =>
            {
                var client = _mobileServiceClient.GetCloudService().GetMobileServiceClientAsync().Result;
                return client.GetSyncTable<T>();
            });
            return Task.Delay(0);
        }
    }
}
