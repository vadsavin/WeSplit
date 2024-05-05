using WeSplit.Common.Secrets.Attributes;

namespace WeSplit.Common.Secrets
{
    public class SourcesHandler
    {
        public string? GetSecret(BaseSourceAttribute attribute)
        {
            return attribute switch
            {
                FromFileAttribute fa => File.ReadAllText(fa.FilePath),
                FromEnvironmentAttribute fe => Environment.GetEnvironmentVariable(fe.Name),
                _ => throw new ArgumentOutOfRangeException("Unsupported secret resolver type"),
            };
        }
    }
}
