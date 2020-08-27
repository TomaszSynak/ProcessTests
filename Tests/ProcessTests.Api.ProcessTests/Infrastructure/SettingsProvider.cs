namespace ProcessTests.Api.ProcessTests.Infrastructure
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    // Consideration: https://stackoverflow.com/questions/6069661/does-system-activator-createinstancet-have-performance-issues-big-enough-to-di
    internal static class SettingsProvider
    {
        private static IConfigurationRoot _settings;

        static SettingsProvider()
        {
            Settings();
        }

        public static T Get<T>()
            where T : new()
        {
            var settings = new T();

            Settings().GetSection(typeof(T).Name).Bind(settings);

            return settings;
        }

        public static IConfigurationRoot Settings()
        {
            return _settings ??= new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("processTestSettings.json")
                .AddJsonFile("processTestSettings.Development.json", true, true)
                .Build();
        }
    }
}
