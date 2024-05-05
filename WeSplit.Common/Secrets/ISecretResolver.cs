namespace WeSplit.Common.Secrets
{
    public interface ISecretResolver
    {
        string TelegramApiKey { get; }

        void Resolve();
    }
}
