namespace MeterReadingModel.View
{
    public class CSVImportProcessedResponse
    {
        public CSVImportProcessedResponse()
        {
            ProcessedSuccessfully = 0;
            FailedToProcess = 0;
        }

        public int ProcessedSuccessfully { get; set; }
        public int FailedToProcess { get; set; }
    }
}
