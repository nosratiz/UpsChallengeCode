using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;

namespace UPS.Common.Logger;

public static class SeriLogConfiguration
{
    public static void SeriLogConfig(this IHost builder, IConfiguration configuration)
    {
        var seriLogSetting = new SeriLogSetting();
        configuration.GetSection("SeriLogSetting").Bind(seriLogSetting);


        new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.Seq(seriLogSetting.SeqUrl)
            .ReadFrom.Configuration(configuration)
            .CreateLogger(); 
    }
}