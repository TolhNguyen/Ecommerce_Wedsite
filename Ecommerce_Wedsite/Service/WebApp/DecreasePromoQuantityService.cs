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
    public interface IDecreasePromoQuantityService // Tạo Interface
    {
        Task<ResponseMessageObject<bool>> DecreasePromoQuantity(int promoid); // cách trả vê là true or false (còn hay k còn sl discount)
    }
    public class DecreasePromoQuantityService : IDecreasePromoQuantityService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public DecreasePromoQuantityService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }
        public async Task<ResponseMessageObject<bool>> DecreasePromoQuantity(int promoid) // có thể sử dụng chung viewmodel đc
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select top 1 * from Promotion where Promotion_Id = {promoid}");

                    var promotion = await query.QueryFirstOrDefaultAsync<Promotion>();

                    if (promotion.Promotion_Quantity > 0) // nếu còn discount 
                    {
                        --promotion.Promotion_Quantity;
                        dbConn.Update(promotion);
                    }
                    else return new ResponseMessageObject<bool> { success = false };

                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                return new ResponseMessageObject<bool> { success = false }; //
            }
            return new ResponseMessageObject<bool> { success = true }; // service này trả về k quan trọng
        }
    }
}
