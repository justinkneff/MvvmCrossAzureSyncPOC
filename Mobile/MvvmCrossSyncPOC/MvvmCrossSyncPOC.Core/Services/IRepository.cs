using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvvmCrossSyncPOC.Core.Services
{
    public interface IRepository<T> where T : class
    {
        Task InsertItemAsync(T item);
        Task InsertItemsAsync(IEnumerable<T> items);

        Task<IEnumerable<T>> GetItemsAsync();

        Task<T> ReadItemAsync(Guid id);
        Task<T> UpdateItemAsync(T item);
        Task DeleteItemAsync(T item);
        Task<ICollection<T>> ReadAllItemsAsync();
        Task<ICollection<T>> ReadItemsAsync(int start, int count);
        Task PullAsync();
    }
}