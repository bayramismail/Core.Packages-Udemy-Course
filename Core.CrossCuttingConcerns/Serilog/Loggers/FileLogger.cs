using Core.CrossCuttingConcerns.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Core.CrossCuttingConcerns.Serilog.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        private readonly IConfiguration _configuration;

        public FileLogger(IConfiguration configuration)
        {
            _configuration = configuration;

            FileLogConfiguration logConfig = configuration.GetSection("SerilLogConfigurations:FileLogConfiguration")
                .Get<FileLogConfiguration>() ?? throw new Exception(SeriLogMessages.NullOptionsMessage);

            string logFilePath = string.Format(format: "{0}{1}", arg0: Directory.GetCurrentDirectory() + logConfig.FolderPath, arg1: ".txt");
           
            Logger = new LoggerConfiguration().WriteTo.File(
                logFilePath,
                rollingInterval: RollingInterval.Day,//hergün yeni bir dosya oluştur demek
                retainedFileCountLimit: null,//Cok Fazla dosya olursa silmek için limit 
                fileSizeLimitBytes: 5000000,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}" //dosya işmi ne olsun
                ).CreateLogger();
        }
    }
}
