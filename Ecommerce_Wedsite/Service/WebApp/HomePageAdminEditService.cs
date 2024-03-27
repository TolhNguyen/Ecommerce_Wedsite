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
    public interface IHomePageAdminEditService // Tạo Interface
    {
        Task<ResponseMessageObject<HomePage_ViewModel>> HomePageAdminEdit(int HomePage_Id); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class HomePageAdminEditService : IHomePageAdminEditService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public HomePageAdminEditService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<HomePage_ViewModel>> HomePageAdminEdit(int HomePage_Id) // Lấy dữ liệu model từ db lên và hành động vào
        {
            var data = new ResponseMessageObject<HomePage_ViewModel>();
            data.Data = new HomePage_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from HomePage where HomePage_Id = {HomePage_Id}"); // lấy thông tin từ id đó

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

