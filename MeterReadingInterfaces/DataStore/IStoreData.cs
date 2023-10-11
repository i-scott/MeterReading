namespace MeterReadingInterfaces.DataStore
{
    public interface IStoreData<in TEntity, TKeyType>
    {
        Task<TKeyType?> SetAsync(TEntity entity);
    }
}
