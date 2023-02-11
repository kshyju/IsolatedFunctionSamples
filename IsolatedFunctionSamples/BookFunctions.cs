using System.Net;
using IsolatedFunctionSamples.Converters;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Converters;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace IsolatedFunctionSamples
{
    public class BookFunctions
    {
        private readonly ILogger<CustomerFunctions> _logger;

        public BookFunctions(ILogger<CustomerFunctions> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Function("GetBook")]
        public HttpResponseData Update(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "books/{id}")] HttpRequestData req,
            [InputConverter(typeof(BookConverter))] Book book)
        {
            _logger.LogInformation($"Processing request for {book.Name}");

            //do something with the customer instance.

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteString($"Processed {book.Name}");

            return response;
        }
    }

    public class Book
    {
        public string Name { set; get; }

        public string Isbn { set; get; }
    }
}
