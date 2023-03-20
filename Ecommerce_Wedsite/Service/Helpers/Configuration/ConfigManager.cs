using Microsoft.Extensions.Configuration;
namespace Ecommerce_Wedsite.Service.Helpers.Configuration
{
    public class ConfigManager : IConfigManager
    {
        private readonly IConfiguration _configuration;
        public ConfigManager(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string GetConnectionString(string connectionName)
        {
            return this._configuration.GetConnectionString(connectionName);
        }

        public IConfigurationSection GetConfigurationSection(string key)
        {
            return this._configuration.GetSection(key);
        }
    }
}
