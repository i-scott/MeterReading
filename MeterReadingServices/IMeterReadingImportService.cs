using System.Threading.Tasks;

namespace MeterReadingServices
{
    public interface IMeterReadingImportService
    {
        Task<int> ImportFromFilesAsync(string[] fileNames);
    }
}