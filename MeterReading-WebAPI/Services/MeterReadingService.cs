using CSVFile;

namespace MeterReadingWebAPI.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private IMeterReadingParser _meterReadingParser;

        public MeterReadingService(IMeterReadingParser meterReadingParser)
        {
            _meterReadingParser = meterReadingParser;
        }

        public int Process(FormFileCollection formFileCollection)
        {
            var filesProcessed = 0;

            foreach( var file in formFileCollection)
            {
                using( CSVReader csvReader = new CSVReader(file.OpenReadStream())){
                    foreach(var line in csvReader) {
                        try
                        {
                            var meterReading = _meterReadingParser.ToMeterReading(line);
                        }
                        catch (Exception ex) { 
                        
                        }
                    }                    
                }                
                filesProcessed++;
            }

            return filesProcessed;
        }
    }
}
