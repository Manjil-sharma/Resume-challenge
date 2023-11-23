using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using Moq; // Add this using directive

namespace tests
{
    public class TestCounter
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void Http_trigger_should_return_known_string()
        {
            var counter = new Company.Function.Counter();
            counter.Id = "index";
            counter.Count = 2;

            // Mock IAsyncCollector<Counter>
            var mockCollector = new Mock<IAsyncCollector<Company.Function.Counter>>();

            var request = TestFactory.CreateHttpRequest();

            // Call the Run method and use await
            var response = (HttpResponseMessage)await Company.Function.GetResumeCounter.Run(request, counter, mockCollector.Object, logger);

            // Deserialize the response content
            var content = await response.Content.ReadAsStringAsync();
            var modifiedCounter = JsonConvert.DeserializeObject<Company.Function.Counter>(content);

            Assert.Equal(3, modifiedCounter.Count);
        }
    }
}
