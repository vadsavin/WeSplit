namespace WeSplit.Common.Settings
{
    public interface ISettings
    {
        string WebDomain { get; }

        string TelegramBotId { get; }

        string SqLiteConnectionString { get; }

        void Initialize();
    }
}
