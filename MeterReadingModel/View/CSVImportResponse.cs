namespace MeterReadingModel.View
{
    public class CSVImportProcessedResponse
    {
        public CSVImportProcessedResponse()
        {
            ProcessedSuccessfully = 0;
            FailedToProcess = 0;
            FilesProcessed = 0;
            FilesNotProcessed = 0;
        }

        public int ProcessedSuccessfully { get; set; }
        public int FailedToProcess { get; set; }
        public int FilesProcessed { get; set; }
        public int FilesNotProcessed { get; set; }
    }
}
