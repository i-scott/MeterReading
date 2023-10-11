using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeterReadingInterfaces.DataStore
{
    public interface IFetchData<TEntity, in TKeyType>
    {
        Task<TEntity?> FetchDataAsync(TKeyType key);

        Task<IList<TEntity>> FetchDataAsync(string query, object? param = null);
    }
}
