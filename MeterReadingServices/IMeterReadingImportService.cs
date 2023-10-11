using MeterReadingModel.View;
using System.Threading.Tasks;

namespace MeterReadingServices
{
    public interface IMeterReadingImportService
    {
        CSVImportProcessedResponse ImportFromFiles(string[] fileNames);
    }
}