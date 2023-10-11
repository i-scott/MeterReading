using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MeterReadingWebAPI.AppServices
{
    public class FormFileToTempStoreUploader : IFormFileUploader
    {
        private readonly string _tempFileLocation;
        private readonly ILogger<FormFileToTempStoreUploader> _logger;

        public FormFileToTempStoreUploader(string tempFileLocation, ILogger<FormFileToTempStoreUploader> logger)
        {
            _tempFileLocation = tempFileLocation;
            _logger = logger;
        }

        public async Task<string[]> UploadFilesAsync(IFormFileCollection formFiles)
        {
            var uploadedFiles = new List<string>();

            try
            {
                string uploads = Path.Combine(_tempFileLocation, "uploads");

                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                foreach (IFormFile file in formFiles)
                {
                    if (file.Length > 0)
                    {
                        string filePath = Path.Combine(uploads, file.FileName);

                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        uploadedFiles.Add(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to upload files to temporary store");
            }

            return uploadedFiles.ToArray();
        }
    }
}
