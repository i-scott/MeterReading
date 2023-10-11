using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingInterfaces.DataStore
{
    public interface IFetchData<TEntity, in TKeyType>
    {
        Task<TEntity> FetchDataAsync(TKeyType key);
    }
}
