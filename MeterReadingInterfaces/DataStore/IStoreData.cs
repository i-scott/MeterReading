namespace MeterReadingInterfaces.DataStore
{
    public interface IStoreData<in TEntity, TKeyType>
    {
        Task<long?> SetAsync(TEntity uuid);
    }
}
