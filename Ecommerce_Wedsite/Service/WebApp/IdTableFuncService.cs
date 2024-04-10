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
    public interface IIdTableFuncService // Tạo Interface
    {
        Task<ResponseMessageObject<string>> IdTableFunction(List<string> idsArray, int HomePage_Id); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class IdTableFuncServiceService : IIdTableFuncService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public IdTableFuncServiceService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<string>> IdTableFunction(List<string> idsArray, int HomePage_Id) // Lấy dữ liệu model từ db lên và hành động vào.
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    string idsarr = String.Join(",", idsArray);
                    var query = dbConn.QueryBuilder($"select top 1 * from HomePage where HomePage_Id = {HomePage_Id}"); // select theo top id
                    var homepage = await query.QueryFirstOrDefaultAsync<HomePage>(); /// du lieu tu db. tạo biến lưu dữ liệu mới vào. Dùng QueryFODA để lấy những column duy nhất ra.

                    homepage.HomePage_ProductDisplayId = idsarr;

                    dbConn.Update(homepage);

                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                return new ResponseMessageObject<string> { success = false, message = e.Message };
            }
            return new ResponseMessageObject<string> { success = true, message = "Thành công" };
        }
    }
}
