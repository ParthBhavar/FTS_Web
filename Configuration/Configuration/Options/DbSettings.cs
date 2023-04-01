namespace FTS.Configuration.Options
{
    public class DbSettings
    {
        public string DbConnection { get; set; }

        public string AzureStorageConnectionString { get; set; }

        public string QueueName { get; set; }

        public string BlobContainer { get; set; }
    }
}
