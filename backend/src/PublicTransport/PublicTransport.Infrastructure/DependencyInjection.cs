using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace PublicTransport.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var otelEndpointString = configuration.GetValue<string>("OTEL_ENDPOINT") ?? "localhost:4317";
        var otelEndpoint = new Uri(otelEndpointString);
        var serviceName = configuration.GetValue<string>("SERVICE_NAME") ?? "PublicTransportApi";

        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddConsole();

            builder.AddOpenTelemetry(options =>
            {
                var resourceBuilder = ResourceBuilder.CreateDefault().AddService(serviceName);
                options.SetResourceBuilder(resourceBuilder);
                options.IncludeFormattedMessage = true;
                options.IncludeScopes = true;
                options.AddOtlpExporter(opt => opt.Endpoint = otelEndpoint);
            });
        });
        
        services.AddStackExchangeRedisCache(options =>
        {
            var connectionString = configuration.GetConnectionString("REDIS");
            connectionString ??= "localhost:6379,password=LocalRedisPassword123!";

            options.Configuration = connectionString;
        });

        services.AddHybridCache(options =>
        {
            var l1CacheExpirationFromConfig = configuration.GetValue<int>("L1_CACHE_EXPIRATION_SECONDS");
            var l2CacheExpirationFromConfig = configuration.GetValue<int>("L2_CACHE_EXPIRATION_SECONDS");
            var l1CacheExpiration =  TimeSpan.FromSeconds(l1CacheExpirationFromConfig);
            var l2CacheExpiration =  TimeSpan.FromSeconds(l2CacheExpirationFromConfig);

            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                LocalCacheExpiration = l1CacheExpiration,
                Expiration = l2CacheExpiration
            };

            options.MaximumPayloadBytes = 1024 * 512; // 512 kB max cache payload
        });
        
        services.AddOpenTelemetry()
            .ConfigureResource(resourceBuilder => resourceBuilder.AddService(serviceName))
            .WithMetrics(config => config
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddOtlpExporter(options => options.Endpoint = otelEndpoint))
            .WithTracing(config => config
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(options => options.Endpoint = otelEndpoint));
        

        return services;
    }
}