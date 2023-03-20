using Microsoft.Extensions.Configuration;
namespace Ecommerce_Wedsite.Service.Helpers.Configuration
{
    public interface IConfigManagerService
    {
        string GetConnectionString(string connectionName);

        IConfigurationSection GetConfigurationSection(string Key);
    }
    public class ConfigManagerService : IConfigManagerService
    {
        private readonly IConfiguration _configuration;
        public ConfigManagerService(IConfiguration configuration)
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
