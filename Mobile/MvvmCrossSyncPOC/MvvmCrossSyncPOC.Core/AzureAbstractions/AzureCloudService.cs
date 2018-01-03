using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Plugin.Connectivity;

namespace MvvmCrossSyncPOC.Core.AzureAbstractions
{
    public class AzureCloudService : ICloudService
    {
        private readonly AppSettings _appSettings;
        private MobileServiceClient client;

        public AzureCloudService(AppSettings appSettings)
        {
            _appSettings = appSettings;
            ReinitializeTables();
        }

        public Task<IMobileServiceClient> GetMobileServiceClientAsync()
        {
            //we run this in the task because if we dont we may hang the loading of the app when it starts.
            try
            {
                Task.Run(() => InitializeAsync()).Wait();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            IMobileServiceClient returnable = client;
            return Task.FromResult(returnable);
        }


        #region Offline Sync Initialization

        async Task InitializeAsync()
        {
            // Short circuit - local database is already initialized
            if (client.SyncContext.IsInitialized)
                return;

            // Create a reference to the local sqlite store
            var store = new MobileServiceSQLiteStore(_appSettings.OfflineFile);

            // Define the database schema
            store.DefineTable<ToDoItem>();
            // tables for case service integration

            // Actually create the store and update the schema
            await client.SyncContext.InitializeAsync(store);
        }

        /// <summary>
        /// The push of the operations queue up to the mobile backend handled by this single call
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task SyncOfflineCacheAsync()
        {
            await InitializeAsync();
            if (!(await CrossConnectivity.Current.IsRemoteReachable(client.MobileAppUri.Host)))
            {
                Debug.WriteLine($"Cannot connect to {client.MobileAppUri} right now - offline");
                return;
            }

            // Push the Operations Queue to the mobile backend
            await client.SyncContext.PushAsync();

            // Pull each sync table
            //var taskTable = await GetTableAsync<T>();
            //await taskTable.PullAsync();
        }

        #endregion

        public Task ReinitializeTables()
        {
            string syncApiHost = "http://db8537d9.ngrok.io";

            client = new MobileServiceClient(
                $"{syncApiHost}",
                new MobileServiceClientAuthHandler())
            {
                SerializerSettings = { CamelCasePropertyNames = false }
            };

            return Task.Delay(0);
        }
    }
}

