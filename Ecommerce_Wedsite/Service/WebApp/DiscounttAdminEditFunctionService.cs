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
    public interface IDiscountAdminEditFunctionService // Tạo Interface
    {
        Task<ResponseMessageObject<string>> Service_Test(Discountt discountitem); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class DiscountAdminEditFunctionService : IDiscountAdminEditFunctionService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public DiscountAdminEditFunctionService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }
        public async Task<ResponseMessageObject<string>> Service_Test(Discountt discountitem) // Lấy dữ liệu model từ db lên và hành động vào.
        {
            //var data = new ResponseMessageObject<Admin_ViewModel>();
            //data.Data. = new Header_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select top 1 * from Discountt where Discount_Id = {discountitem.Discount_Id}"); // select theo top id
                    var discount = await query.QueryFirstOrDefaultAsync<Discountt>(); /// du lieu tu db. tạo biến lưu dữ liệu mới vào. Dùng QueryFODA để lấy những column duy nhất ra.

                    //  map du lieu db  cli
                    //  mapper 
                    // map tay 

                    discount.Discount_Rate = discountitem.Discount_Rate;
                    discount.Discount_No = discountitem.Discount_No;
                    discount.Discount_RanId = discountitem.Discount_RanId;
                    discount.Discount_Name = discountitem.Discount_Name;

                    var it = dbConn.Update(discount);

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


