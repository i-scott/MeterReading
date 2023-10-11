using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Net.WebSockets;
using MeterReadingServices;
using MeterReadingWebAPI.AppServices;

namespace MeterReadingWebAPI.Controllers
{

    [ApiController]
    public class ReadingsController : ControllerBase
    {
        private readonly IFormFileUploader _formFileUploader;
        private readonly ILogger<ReadingsController> _logger;

        public ReadingsController(IFormFileUploader formFileUploader, IMeterReadingImportService meterReadingImportService, ILogger<ReadingsController> logger)
        {
            _formFileUploader = formFileUploader;
            _logger = logger;
        }

        [HttpPost()]
        [MapToApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/meter-reading-uploads")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [SwaggerResponseHeader(StatusCodes.Status201Created, "Location", "URI", "Location to the newly created catalogue item")]
        [SwaggerOperation(Summary = "Upload Meter Reading Data in CSV Format",
                            Description = "Uploads, validates and Creates Meter Reading Data",
                            OperationId = "meter-reading-uploads")]
        public async Task<IActionResult> MeterReadingUpload([FromForm] IFormFileCollection files)
        {
            if (files == null || files.Count == 0) return BadRequest("No file given to upload");

            if(!files[0].FileName.ToLower().EndsWith("csv")) return BadRequest("You must send a csv file");

            if (files.Count > 1) return BadRequest("We can only process one file at a time");

            try
            {
                var uploadedFiles = await _formFileUploader.UploadFilesAsync(files);

                //var result = 

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Meter Reading Upload failed" );
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
