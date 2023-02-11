using Microsoft.Azure.Functions.Worker.Converters;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace IsolatedFunctionSamples.Converters
{
    public class BookConverter : IInputConverter
    {
        static readonly HttpClient httpClient = new();

        private readonly ILogger<BookConverter> _logger;

        public BookConverter(ILogger<BookConverter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask<ConversionResult> ConvertAsync(ConverterContext context)
        {

            if (context.FunctionContext.BindingContext.BindingData.TryGetValue("id", out var idObj))
            {
                var id = Convert.ToInt32(idObj);
                try
                {
                    var customer = await GetBookFromRestAPIAsync(id);
                    if (customer is not null)
                    {
                        _logger.LogInformation($"Populated Customer instance for {id}");

                        return ConversionResult.Success(customer);
                    }
                }
                catch (Exception ex)
                {
                    return ConversionResult.Failed(ex);
                }
            }

            return ConversionResult.Unhandled();
        }

        private async Task<Book?> GetBookFromRestAPIAsync(int id)
        {
            var url = $"https://anapioficeandfire.com/api/books/{id}";

            return await httpClient.GetFromJsonAsync<Book?>(url);
        }
    }
}
