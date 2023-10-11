namespace MeterReadingInterfaces.DataStore
{
    public interface IStoreData<in TEntity, out TKeyType>
    {
        TKeyType Set(TEntity entity);
    }
}
