using Microsoft.Extensions.Configuration;

namespace Ecommerce_Wedsite.Service.Helpers.Configuration
{
    public interface IConfigManager
    {
        string GetConnectionString(string connectionName);

        IConfigurationSection GetConfigurationSection(string Key);
    }
}
