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
    public interface IDiscountNumberFunctionService // Tạo Interface
    {
        Task<ResponseMessageObject<bool>> DiscountNumberFunction(string? discountranid); // cách trả vê là true or false (còn hay k còn sl discount)
    }
    public class DiscountNumberFunctionService : IDiscountNumberFunctionService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public DiscountNumberFunctionService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        // có thể sử dụng chung viewmodel đc
        public async Task<ResponseMessageObject<bool>> DiscountNumberFunction(string? discountranid) // Chức năng: lấy random id lên ss và tính số lượng
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select top 1 * from Discountt where Discount_NewId = '{discountranid}'");

                    var discount = await query.QueryFirstOrDefaultAsync<Discountt>();

                    if(discount.Discount_No > 0) // nếu còn discount 
                    {
                        --discount.Discount_No;
                        dbConn.Update(discount);
                    } else return new ResponseMessageObject<bool> { success = false };

                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                return new ResponseMessageObject<bool> { success = false}; //false = hết => k discount được
            }
            return new ResponseMessageObject<bool> { success = true }; // true = còn => giảm discount thành công
        }
    }
}
