using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface INewWebAppService
    {
        Task<ResponseMessage<string>> Service_Test();
    }
    public class NewWebAppService : INewWebAppService
    {
        private readonly IConfigManagerService _configuration;
        public NewWebAppService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessage<string>> Service_Test()
        {
            var data = new ResponseMessage<string>();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString_Customers")))
                {
                    await dbConn.OpenAsync();

                    var query = dbConn.QueryBuilder($"select top 2000000 id as customers_id ,sha256_e164 as sha256 from data_customers cus ");
                    data.Data = await query.QueryAsync<string>();

                    await dbConn.CloseAsync();
                }
            }
            catch(Exception e)
            {
                data.message = e.Message;
                return data;
            }
            
            return data;
        }
    }
}
