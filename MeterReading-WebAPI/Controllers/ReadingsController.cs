using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;

namespace Controllers {

    [ApiController]
    public class ReadingsController : ControllerBase
    {
        private readonly ILogger<ReadingsController> _logger;

        public ReadingsController(ILogger<ReadingsController> logger)
        {
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
        public IActionResult MeterReadingUpload([FromForm] IFormFileCollection files)
        {
            if (files == null || files.Count == 0) return BadRequest("No file given to upload");
                
            return Ok();            
        }
    }
}
