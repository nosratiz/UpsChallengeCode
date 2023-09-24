using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;

namespace UPS.Common.Logger;

public static class SeriLogConfiguration
{
    public static void SeriLogConfig(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var seriLogSetting = new SeriLogSetting();
        configuration.GetSection("SeriLogSetting").Bind(seriLogSetting);


        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.Seq(seriLogSetting.SeqUrl)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        builder.Host.UseSerilog(logger);
    }
}