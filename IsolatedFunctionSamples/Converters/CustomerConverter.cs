using Microsoft.Azure.Functions.Worker.Converters;
using System.Net.Http.Json;

namespace IsolatedFunctionSamples.Converters
{
    public class CustomerConverter : IInputConverter
    {
        static readonly HttpClient httpClient = new();
        public async ValueTask<ConversionResult> ConvertAsync(ConverterContext context)
        {
            if (context.FunctionContext.BindingContext.BindingData.TryGetValue("id", out var idObj))
            {
                var id = Convert.ToInt32(idObj);
                try
                {
                    var customer = await GetCustomerFromRestAPIAsync(id);
                    if (customer is not null)
                    {
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

        private async Task<Customer?> GetCustomerFromRestAPIAsync(int id)
        {
            var url = $"https://anapioficeandfire.com/api/characters/{id}";

            return await httpClient.GetFromJsonAsync<Customer?>(url);
        }
    }
}
