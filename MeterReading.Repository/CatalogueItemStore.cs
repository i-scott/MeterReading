using MeterReadingModel;
using MeterReadingInterfaces.DataStore;

namespace MeterReadingRepository
{
    public class MeterReadingStore : IStoreData<MeterReading, long>
    {
        public MeterReadingStore() { }
        public Task<long> SetAsync(MeterReading meterReading)
        {
            throw new NotImplementedException();
        }
    }
}
