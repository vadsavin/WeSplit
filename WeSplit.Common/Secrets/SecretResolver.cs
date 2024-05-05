using System.Reflection;
using WeSplit.Common.Secrets.Attributes;

namespace WeSplit.Common.Secrets
{
    public class SecretResolver : ISecretResolver
    {
        private SourcesHandler _sourcesHandler;

        [FromFile("telegram.token")]
        public string TelegramApiKey { get; private set; }

        public SecretResolver()
        {
            _sourcesHandler = new SourcesHandler();
            Resolve();
        }

        public void Resolve()
        {
            var secretsToResolve = GetType().GetProperties()
                .Where(p => p.GetCustomAttribute<BaseSourceAttribute>() != null)
                .ToList();

            Console.WriteLine($"Will resolve [{secretsToResolve.Count}] secrets");

            foreach (var property in secretsToResolve)
            {
                var resolverAttribute = property.GetCustomAttribute<BaseSourceAttribute>();
                property.SetValue(this, _sourcesHandler.GetSecret(resolverAttribute));
            }
        }
    }
}
