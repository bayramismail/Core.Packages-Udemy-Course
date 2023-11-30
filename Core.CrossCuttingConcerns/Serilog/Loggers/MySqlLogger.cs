using Core.CrossCuttingConcerns.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace Core.CrossCuttingConcerns.Serilog.Loggers;

public class MySqlLogger:LoggerServiceBase
{
    private readonly IConfiguration _configuration;

    public MySqlLogger(IConfiguration configuration)
    {
        _configuration = configuration;
        MySqlLogConfiguration loggeConfiguration = _configuration.GetSection("SerilLogConfigurations:MysqlConfiguration")
          .Get<MySqlLogConfiguration>() ?? throw new Exception(SeriLogMessages.NullOptionsMessage);
        Logger seriLogConfig = new LoggerConfiguration()
            .WriteTo.MySQL(
            loggeConfiguration.ConnectionString,
            tableName:loggeConfiguration.TableName,
            storeTimestampInUtc:loggeConfiguration.StoreTimestampInUtc
            )
            .CreateLogger();
        Logger = seriLogConfig;
    }
}
