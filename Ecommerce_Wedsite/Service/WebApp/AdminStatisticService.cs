using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IAdminStatisticService // Tạo Interface
    {
        Task<ResponseMessageObject<AdminStatistic_ViewModel>> AdminStatistic();
    }
    public class AdminStatisticService : IAdminStatisticService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public AdminStatisticService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }
        public async Task<ResponseMessageObject<AdminStatistic_ViewModel>> AdminStatistic() // Code cho Phương Thức Service
        {
            var data = new ResponseMessageObject<AdminStatistic_ViewModel>(); // Tạo biến dữ liệu. data là biến lớn của RMO
            data.Data = new AdminStatistic_ViewModel(); // Tạo biến lưu trữ để Data Không lỗi báo rỗng. Data là biến nhỏ hơn là HVM. (Lỗi cơ bản)
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select top 5 ProductCheckout_Name, SUM(ProductCheckout_Quantity) as TongSoLuong from ProductCheckout GROUP BY ProductCheckout_Name order by SUM(ProductCheckout_Quantity) DESC"); // thao tác querry 
                    data.Data.productcheckoutstatistic = await query.QueryAsync<ProductCheckoutStatistic>();

                    var query2 = dbConn.QueryBuilder($"SELECT COUNT(Product_Id) AS TongSoLuongSP FROM Product"); // thao tác querry 
                    data.Data.productcountstatistic = await query2.QueryAsync<ProductCountStatistic>();

                    var query3 = dbConn.QueryBuilder($"SELECT COUNT(CustomerCheckout_Id) AS TongSoLuongCS FROM CustomerCheckout"); // thao tác querry 
                    data.Data.customercountstatistic = await query3.QueryAsync<CustomerCountStatistic>();

                    var query4 = dbConn.QueryBuilder($"SELECT SUM(CustomerCheckout_TotalPrice) AS TongDoanhThu FROM CustomerCheckout;"); // thao tác querry 
                    data.Data.turnoverstatistic = await query4.QueryAsync<TurnoverStatistic>();

                    var query5 = dbConn.QueryBuilder($"SELECT COUNT(Promotion_Id) AS TongPromoSuDung FROM CustomerCheckout WHERE Promotion_Id <> 0"); // thao tác querry 
                    data.Data.promousedstatistic = await query5.QueryAsync<PromotionUsedStatistic>();

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

