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
    public interface IHomePageAdminService // Tạo Interface
    {
        Task<ResponseMessageObject<HomePage_ViewModel>> HomePageAdmin(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class HomePageAdminService : IHomePageAdminService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public HomePageAdminService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<HomePage_ViewModel>> HomePageAdmin() // có thể sử dụng chung viewmodel đc
        {
            var data = new ResponseMessageObject<HomePage_ViewModel>();
            data.Data = new HomePage_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from HomePage");

                    data.Data.homepage = await query.QueryAsync<HomePage>();

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


