using MeterReadingModel.View;
using System.Threading.Tasks;

namespace MeterReadingServices
{
    public interface IMeterReadingImportService
    {
        Task<CSVImportProcessedResponse> ImportFromFilesAsync(string[] fileNames);
    }
}