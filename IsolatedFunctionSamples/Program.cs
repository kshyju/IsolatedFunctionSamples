using IsolatedFunctionSamples.Converters;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(workerOptions =>
    {
        workerOptions.InputConverters.Register<CustomerConverter>();
    })
    .Build();

await host.RunAsync();
