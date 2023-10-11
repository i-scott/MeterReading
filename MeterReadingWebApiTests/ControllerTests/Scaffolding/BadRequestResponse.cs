using System.Collections.Generic;

namespace MeterReading_WebApiTests.ControllerTests.Scaffolding
{
    public class BadRequestResponse
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int Status { get; set; }

        public string? TraceId { get; set; }

        public Error? Errors { get; set; }
    }

    public class Error
    {
        public List<string>? Name { get; set; }
    }
}
