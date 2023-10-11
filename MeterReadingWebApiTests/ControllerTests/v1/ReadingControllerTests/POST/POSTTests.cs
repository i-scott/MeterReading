using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MeterReading_WebApiTests.ControllerTests.Scaffolding;
using MeterReadingWebAPI.Controllers;
using Xunit;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace MeterReading_WebApiTests.ControllerTests.v1.ReadingControllerTests.POST
{
    public class POSTTests : IClassFixture<CustomWebApplicationFactory<POSTScenarioStartup<ReadingsController>>>
    {
        private readonly HttpClient _httpClient;

        public POSTTests(CustomWebApplicationFactory<POSTScenarioStartup<ReadingsController>> factory)
        {
            _httpClient = factory.CreateClient();
        }

        public static MultipartFormDataContent CreateTestFile(string fileName, string fileContent)
        {
            var content = new MultipartFormDataContent();
            var byteArrayContent = new ByteArrayContent(Encoding.UTF8.GetBytes(fileContent));
            byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            content.Add(byteArrayContent, "files", fileName);

            return content;
        }

        [Fact]
        public async Task WhenGivenAFile_ReturnsOkResponse()
        {
            var testData = "test,test,test";

            var result = await _httpClient.PostAsync("/api/v1/meter-reading-uploads", CreateTestFile("test.csv", testData));

            result.IsSuccessStatusCode.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task WhenNotGivenAFile_ReturnsBadRequest()
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("Partner1"), "EntityId");

            var result = await _httpClient.PostAsync("/api/v1/meter-reading-uploads", content);

            result.IsSuccessStatusCode.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task WhenGivenMultipleCsvFiles_ReturnsOkRequest()
        {
            var testData = "test,test,test";

            var testFiles = CreateTestFile("test.csv", testData);

            var byteArrayContent = new ByteArrayContent(Encoding.UTF8.GetBytes(testData));
            byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            testFiles.Add(byteArrayContent, "files", "test.csv");
            testFiles.Add(byteArrayContent, "files", "test.csv");

            var result = await _httpClient.PostAsync("/api/v1/meter-reading-uploads", testFiles);

            result.IsSuccessStatusCode.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task WhenGivenNONCsvFile_ReturnsBadRequest()
        {
            var testData = "test,test,test";

            var result = await _httpClient.PostAsync("/api/v1/meter-reading-uploads", CreateTestFile("test.png", testData));

            result.IsSuccessStatusCode.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


    }
}
    

