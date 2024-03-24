using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IHomePageService // Tạo Interface
    {
        // Dùng 1 model nhỏ HVM để Service chạy lệnh query của Header
        //RMObject chỉ lấy từng 1 dữ liệu ( vd là model có từng danh sách 1 như Header_ViewModel)
        // Sẽ là RM khi lấy toàn bộ dữ liệu cùng lúc (vd là model có nhiều dữ liệu đi chung như Header)
        Task<ResponseMessageObject<HomePage_ViewModel>> HomePage_ServiceTest(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class HomePageService : IHomePageService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public HomePageService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<HomePage_ViewModel>> HomePage_ServiceTest() // Code cho Phương Thức Service
        {
            var data = new ResponseMessageObject<HomePage_ViewModel>(); // Tạo biến dữ liệu. data là biến lớn của RMO
            data.Data = new HomePage_ViewModel(); // Tạo biến lưu trữ để Data Không lỗi báo rỗng. Data là biến nhỏ hơn là HVM. (Lỗi cơ bản)
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from HomePage where HomePage_Type = 1 and HomePage_Levels = 1 order by HomePage_Order asc"); // thao tác querry 
                    //data.Data = await query.QueryAsync<AllLayout>(); // lưu vào dữ liệu

                    data.Data.homepage_Vl1 = await query.QueryAsync<HomePage>(); // chỉ lấy mỗi 1 header_Lv1 nên là dạng Object
                    // Vào biến lớn "data." đến biến nhỏ hơn "Data." sau đó vào danh sách nhỏ nữa là "header_VL1(trong model header_ViewModel"

                    query = dbConn.QueryBuilder($"select * from HomePage where HomePage_Type = 1 and HomePage_Levels = 2"); // thao tác querry

                    //data.Data = await query.QueryAsync<AllLayout>(); // lưu vào dữ liệu
                    data.Data.homepage_Vl2 = await query.QueryAsync<HomePage>(); // lấy dữ liệu từ Entities Header (để có title riêng của header_lv1 2 ra)

                    query = dbConn.QueryBuilder($"select * from HomePage");

                    data.Data.picture_Id = await query.QueryAsync<HomePage>();

                    query = dbConn.QueryBuilder($"SELECT TOP 4 * FROM Product ORDER BY Product_Id DESC"); // thao tác querry 
                    
                    // Select sp lên:
                    data.Data.newproduct = await query.QueryAsync<Product>();
                    // Vào biến lớn "data." đến biến nhỏ hơn "Data." sau đó vào danh sách nhỏ nữa là "header_VL1(trong model header_ViewModel"
                    //Gom nguyên một danh sách title này thành 1 object gọi là header_lv1 2

                    query = dbConn.QueryBuilder($"SELECT * FROM Product where Product_PromoPrice != 0"); // thao tác querry 

                    //data.Data = await query.QueryAsync<AllLayout>(); // gán và lưu vào dữ liệu
                    data.Data.promoproduct = await query.QueryAsync<Product>();

                    query = dbConn.QueryBuilder($"select * from Users");

                    data.Data.users = await query.QueryAsync<Users>();

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
