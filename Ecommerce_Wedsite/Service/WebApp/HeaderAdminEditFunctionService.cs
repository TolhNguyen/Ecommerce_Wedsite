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
    public interface IHeaderAdminEditFunctionService // Tạo Interface
    {
        Task<ResponseMessageObject<string>> Service_Test(Header headeritem); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class HeaderAdminEditFunctionService : IHeaderAdminEditFunctionService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public HeaderAdminEditFunctionService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }


        /* Khi update db sẽ có 2 kiểu:
         *  - Update cả 1 list:
         *      + ta truyền cả Model biến mới (Header headeritem), có biến data
         *      + k cần query
         *      + chỉ cần 1 câu: var it = dbConn.Update(headeritem);
         *      + trả data như bth
         *  - Update từng column: như dưới
         *      + k cần trả về Model, chỉ cần string (hiện thông báo)
         *      + cần query, dùng mapper
         *      + khi 1: ta cần thay đổi những giá trị số, đặc biệt (như bit trên db). vd: 1 -> 9 trong db là 1 chuỗi string tương ứng trên controller
         *      thì ta cần phải thay đổi từng giá trị như dưới
         *      + Khi 2: cho người dùng đc phải thay đổi những gì mình muốn (gọi lên những col cần thay đổi thôi) k cần select hết (bị thừa)
         *      select col càn update thôi.
         *  - Lưu ý: nên select top sẽ lấy những row cần thiết thôi, code chạy nhanh hơn dựa theo 1 trường (như là id)
         */
        public async Task<ResponseMessageObject<string>> Service_Test(Header headeritem) // Lấy dữ liệu model từ db lên và hành động vào.
        {
            //var data = new ResponseMessageObject<Admin_ViewModel>();
            //data.Data. = new Header_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select top 1 * from Header where Header_Id = {headeritem.Header_Id}"); // select theo top id
                    var header = await query.QueryFirstOrDefaultAsync<Header>(); /// du lieu tu db. tạo biến lưu dữ liệu mới vào. Dùng QueryFODA để lấy những column duy nhất ra.

                    //  map du lieu db  cli
                    //  mapper 
                    // map tay 

                    header.Header_Title = headeritem.Header_Title;
                    header.HeaderType_Id = headeritem.HeaderType_Id;
                    header.HeaderAction = headeritem.HeaderAction;
                    header.HeaderCondition = headeritem.HeaderCondition;
                    header.Header_HTML = headeritem.Header_HTML;
                    header.Header_Order = headeritem.Header_Order;
                    header.Header_Summary = headeritem.Header_Summary;
                    header.Header_Img = headeritem.Header_Img;

                    var it = dbConn.Update(header);

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

