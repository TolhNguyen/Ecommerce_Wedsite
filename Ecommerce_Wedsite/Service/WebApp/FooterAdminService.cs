using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IFooterAdminService // Tạo Interface
    {
        Task<ResponseMessageObject<Footer_ViewModel>> Service_Test(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class FooterAdminService : IFooterAdminService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public FooterAdminService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<Footer_ViewModel>> Service_Test() // có thể sử dụng chung viewmodel đc
        {
            var data = new ResponseMessageObject<Footer_ViewModel>();
            data.Data = new Footer_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Footer");

                    data.Data.footer = await query.QueryAsync<Footer>();

                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.success = false;
            }
            return data;
        }
    }
}


