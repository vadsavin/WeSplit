namespace WeSplit.Common.Settings
{
    public static class SettingsProvider
    {
        private static ISettings _settings;
        private static string _settingsEnvironmentName = "WESPLIT_ENVIRONMENT";

        public static ISettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = GetSettings();
                    _settings.Initialize();
                }

                return _settings;
            }
        }

        private static ISettings GetSettings()
        {
            var environment = Environment.GetEnvironmentVariable(_settingsEnvironmentName);

            return environment switch
            {
                "dev" => new DevSettings(),
                "prod" => new ProdSettings(),
                _ => new DevSettings(),
            };
        }
    }
}
