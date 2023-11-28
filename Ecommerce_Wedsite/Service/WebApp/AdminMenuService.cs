using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IAdminMenuService // Tạo Interface
    {
        // Dùng 1 model nhỏ HVM để Service chạy lệnh query của Header
        //RMObject chỉ lấy từng 1 dữ liệu ( vd là model có từng danh sách 1 như Header_ViewModel)
        // Sẽ là RM khi lấy toàn bộ dữ liệu cùng lúc (vd là model có nhiều dữ liệu đi chung như Header)
        Task<ResponseMessageObject<AdminMenu_ViewModel>> AdminMenu_ServiceTest(); //Model lớn chứa Model nhỏ. Tạo Phương Thức để Controller gọi ra dùng   
    }
    public class AdminMenuService : IAdminMenuService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public AdminMenuService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<AdminMenu_ViewModel>> AdminMenu_ServiceTest() // Code cho Phương Thức Service. Gom code cho các functions chung 1 service
        {
            var data = new ResponseMessageObject<AdminMenu_ViewModel>(); // Tạo biến dữ liệu. data là biến lớn của RMO
            data.Data = new AdminMenu_ViewModel(); // Tạo biến lưu trữ để Data Không lỗi báo rỗng. Data là biến nhỏ hơn là HVM
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync
                    var query = dbConn.QueryBuilder($"select * from AdminMenu where AdminMenu_Condition = 1 and AdminMenu_Level = 1");
                    data.Data.adminmenu_LV1 = await query.QueryAsync<AdminMenu>();
                    query = dbConn.QueryBuilder($"select * from AdminMenu where AdminMenu_Condition = 1 and AdminMenu_Level = 2"); // thao tác querry
                    data.Data.adminmenu_LV2 = await query.QueryAsync<AdminMenu>(); //lưu vào biến
                    await dbConn.CloseAsync();
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
