using Core.CrossCuttingConcerns.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.Serilog.Loggers;

public class MsSqlLogger : LoggerServiceBase
{
    private readonly IConfiguration _configuration;

    public MsSqlLogger(IConfiguration configuration)
    {
        _configuration = configuration;
        MsSqlLoggerConfiguration loggeConfiguration = _configuration.GetSection("SerilLogConfigurations:MssqlConfiguration")
            .Get<MsSqlLoggerConfiguration>() ?? throw new Exception(SeriLogMessages.NullOptionsMessage);
        MSSqlServerSinkOptions sinkOptions = new()
        {
            TableName = loggeConfiguration.TableName,
            AutoCreateSqlTable = loggeConfiguration.AutoCreateSqlTable,
        };
        ColumnOptions columnOptions = new();

        Logger seriLogConfig = new LoggerConfiguration()
            .WriteTo
            .MSSqlServer(loggeConfiguration.ConnectionString,sinkOptions:sinkOptions,columnOptions:columnOptions)
            .CreateLogger();
        Logger = seriLogConfig;
    }
}
