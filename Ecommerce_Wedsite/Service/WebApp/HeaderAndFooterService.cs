using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IHeaderAndFooterService // Tạo Interface
    {
        // Dùng 1 model nhỏ HVM để Service chạy lệnh query của Header
        //RMObject chỉ lấy từng 1 dữ liệu ( vd là model có từng danh sách 1 như Header_ViewModel)
        // Sẽ là RM khi lấy toàn bộ dữ liệu cùng lúc (vd là model có nhiều dữ liệu đi chung như Header)
        Task<ResponseMessageObject<HeaderAndFooter_ViewModel>> HeaderAndFooter_ServiceTest(); //Model lớn chứa Model nhỏ. Tạo Phương Thức để Controller gọi ra dùng   
    }
    public class HeaderAndFooterService : IHeaderAndFooterService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public HeaderAndFooterService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<HeaderAndFooter_ViewModel>> HeaderAndFooter_ServiceTest() // Code cho Phương Thức Service. Gom code cho các functions chung 1 service
        {
            var data = new ResponseMessageObject<HeaderAndFooter_ViewModel>(); // Tạo biến dữ liệu. data là biến lớn của RMO
            data.Data = new HeaderAndFooter_ViewModel(); // Tạo biến lưu trữ để Data Không lỗi báo rỗng. Data là biến nhỏ hơn là HVM
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Header where HeaderType_Id = 1 and HeaderLevels = 1"); // thao tác querry 
                    // Gọi lên các thông tin dữ liệu thuộc các "header cha" (là lv1)

                    //data.Data = await query.QueryAsync<AllLayout>(); // lưu vào dữ liệu
                    data.Data.header_LV1 = await query.QueryAsync<Header>();
                    // Vào biến lớn "data." đến biến nhỏ hơn "Data." sau đó vào danh sách nhỏ nữa là "header_VL1(trong model header_ViewModel"
                    //Gom nguyên một danh sách title này thành 1 object gọi là header_lv1 2

                    query = dbConn.QueryBuilder($"select * from Header where HeaderType_Id = 1 and HeaderLevels = 2"); // thao tác querry
                    //Gọi lên các thông tin dữ liệu thuộc các "header con" (là lv2) 
                    // Nếu có header con nhỏ nữa thì sẽ là lv3, 4, ...

                    //data.Data = await query.QueryAsync<AllLayout>(); // lưu vào dữ liệu
                    data.Data.header_LV2 = await query.QueryAsync<Header>();

                    var query2 = dbConn.QueryBuilder($"select * from Footer where FooterType_Id = 1 and FooterLevels = 1"); 
                    
                    data.Data.footer_Vl1 = await query2.QueryAsync<Footer>();
                    

                    query2 = dbConn.QueryBuilder($"select * from Footer where FooterType_Id = 1 and FooterLevels = 2"); 
                    
                    data.Data.footer_Vl2 = await query2.QueryAsync<Footer>();

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

