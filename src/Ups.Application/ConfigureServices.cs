using System;
using System.Net;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using UPS.Application.Core.Interfaces;
using UPS.Application.Core.Services;
using UPS.Common.Options;
using UPS.Common.Serializers;

namespace UPS.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
    {      
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddScoped<IUserService, UserService>();

        services.AddSingleton<IJsonSerializer, TextJsonSerializer>();
        
        
        services.AddValidatorsFromAssemblyContaining(typeof(IUserService));
        services.Configure<UpsApiService>(configuration.GetSection("UpsApiService"));
        
        string secretApiKey = configuration["ApiKey"]!;
        
        var upsApiService = new UpsApiService();
        configuration.Bind(nameof(Common.Options.UpsApiService), upsApiService);

        upsApiService.ApiKey = secretApiKey;
        
        
        services.AddSingleton(upsApiService);

        services
            .AddHttpClient(
                "UpsApiService",
                c =>
                {
                    c.BaseAddress = new Uri(upsApiService.BaseUrl);
                    c.Timeout = TimeSpan.FromMinutes(1);
                    
                    c.DefaultRequestHeaders.Add(
                        "Authorization",
                        $"Bearer {upsApiService.ApiKey}"
                    );
                }
            )
            .AddTransientHttpErrorPolicy(x =>
            {
                x.OrResult(
                    r =>
                         r.StatusCode >= HttpStatusCode.InternalServerError
                );
                return x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000));
            });
        
        return services;
    }
}