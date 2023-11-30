namespace Core.CrossCuttingConcerns.Serilog.ConfigurationModels;

public class MySqlLogConfiguration
{
    public string ConnectionString { get; set; }
    public string TableName { get; set; }
    public bool StoreTimestampInUtc { get; set; }
    public MySqlLogConfiguration()
    {
        ConnectionString = string.Empty;

        TableName = string.Empty;

    }

    public MySqlLogConfiguration(string connectionString, string tableName, bool storeTimestampInUtc)
    {
        ConnectionString = connectionString;
        TableName = tableName;
        StoreTimestampInUtc = storeTimestampInUtc;
    }
}
