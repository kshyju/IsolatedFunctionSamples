using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace IsolatedFunctionSamples
{
    public class CustomerFunctions
    {
        private readonly ILogger<CustomerFunctions> _logger;

        public CustomerFunctions(ILogger<CustomerFunctions> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Function("Update")]
        public HttpResponseData Update(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers/{id}")] HttpRequestData req,
            Customer customer)
        {
            _logger.LogInformation($"Processing request for {customer.Name}");

            //do something with the customer instance.

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteString($"Processed {customer.Name}");

            return response;
        }
    }

    public class Customer
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public string Culture { get; set; }
    }
}
