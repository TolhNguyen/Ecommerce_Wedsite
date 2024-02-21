using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface ISubHeaderService // Tạo Interface
    {
        Task<ResponseMessageObject<SubHeader_ViewModel>> SubHeader(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class SubHeaderService : ISubHeaderService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public SubHeaderService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<SubHeader_ViewModel>> SubHeader()
        {
            var data = new ResponseMessageObject<SubHeader_ViewModel>();
            data.Data = new SubHeader_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"SELECT * FROM ProductType");
                    data.Data.subheaderproducttype = await query.QueryAsync<ProductType>();

                    // Công thức câu query chọn ra 5 sp của mỗi loại type id khác nhau:
                    var query2 = dbConn.QueryBuilder($"SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY ProductType_Id ORDER BY NEWID()) AS RowNum FROM Product) AS RankedData WHERE RowNum <= 5;");
                    data.Data.subheaderproduct = await query2.QueryAsync<Product>();

                    var query3 = dbConn.QueryBuilder($"SELECT * FROM NewsCategory");
                    data.Data.subheadernewscate = await query3.QueryAsync<NewsCategory>();

                    await dbConn.CloseAsync(); // đóng sync sau khi sử dụng
                }
            }
            catch (Exception e) // Gặp lỗi
            {
                data.message = e.Message;
                data.success = false;
            }
            return data;
        }
    }
}
