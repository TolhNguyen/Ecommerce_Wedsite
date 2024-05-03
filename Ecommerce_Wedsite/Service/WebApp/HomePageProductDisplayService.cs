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
    public interface IHomePageProductDisplayService // Tạo Interface
    {
        Task<ResponseMessageObject<HomePageProductDisplay_ViewModel>> GetProductDisplay(IEnumerable<HomePage> homepage_Vl1); // truyền List LV1 vào
    }
    public class HomePageProductDisplayService : IHomePageProductDisplayService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public HomePageProductDisplayService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<HomePageProductDisplay_ViewModel>>GetProductDisplay(IEnumerable<HomePage> homepage_Vl1)
        {
            var data = new ResponseMessageObject<HomePageProductDisplay_ViewModel>(); 
            data.Data = new HomePageProductDisplay_ViewModel(); 
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync
                    
                    foreach (var p in homepage_Vl1) // duyệt qua từng LV1.
                    {
                        if(p.HomePage_ProductDisplayId == null) continue; // nếu có cái null thì pass 
						p.HomePage_ProductDisplayId = p.HomePage_ProductDisplayId.Trim('(', ')'); // bỏ khoảng trắng
						List<int> ids = p.HomePage_ProductDisplayId.Split(',').Select(int.Parse).ToList(); // convert thành list int
						var query = dbConn.QueryBuilder($"select * from Product where Product_Id in {ids} and Product_Condition = 1"); 
						var item = new ProducdDisplay(); // mỗi lần sẽ tạo mới item chứa
						item.productdisplay = await query.QueryAsync<Product>();
						item.HomePage_Id = p.HomePage_Id;
						data.Data.productdisplayGrp.Add(item); // add item vào list mỗi lần dồn vào
					}
					
					await dbConn.CloseAsync(); // đóng sync 
                }
            }
            catch (Exception e) // Gặp lỗi, sai
            {
                data.message = e.Message;
                data.success = false;
            }
            return data; // gửi data ra ngoài controller
        }
    }
}
