namespace WeSplit.Common.Settings
{
    public class BaseSettings : ISettings
    {
        public string WebDomain { get; } = "https://localhost:5000";

        public string TelegramBotId { get; } = "wesplit_bot";

        public string SqLiteConnectionString { get; } = "Data Source=wesplit.sqlite";

        public virtual void Initialize()
        {
        }
    }
}
