using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MeterReadingWebAPI.AppServices;

public interface IFormFileUploader
{
    Task<string[]> UploadFilesAsync(IFormFileCollection formFiles);
}