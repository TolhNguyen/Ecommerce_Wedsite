//using DapperQueryBuilder;
//using Ecommerce_Wedsite.Models.Entities;
//using Ecommerce_Wedsite.Models.Helpers.Response;
//using Ecommerce_Wedsite.Models.ViewModel;
//using Ecommerce_Wedsite.Service.Helpers.Configuration;
//using Microsoft.AspNetCore.DataProtection;
//using System.Data.SqlClient;

//namespace Ecommerce_Wedsite.Service.WebApp
//{
//    public interface IGetPromoIdService // Tạo Interface
//    {

//        Task<ResponseMessageObject<Promo_ViewModel>> Get_PromoId(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
//    }
//    public class GetPromoIdService : IGetPromoIdService // Thừa kế các thuộc tính từ Interface 
//    {
//        private readonly IConfigManagerService _configuration;
//        public GetPromoIdService(IConfigManagerService configuration)
//        {
//            _configuration = configuration;
//        }

//        public async Task<ResponseMessageObject<Promo_ViewModel>> Get_PromoId()
//        {
//            var data = new ResponseMessageObject<ProductType_ViewModel>();
//            data.Data = new Promo_ViewModel();
//            try
//            {
//                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
//                {
//                    await dbConn.OpenAsync(); // mở sync

//                    var query = dbConn.QueryBuilder($"SELECT * FROM ProductType");

//                    data.Data.producttype = await query.QueryAsync<ProductType>();

//                    await dbConn.CloseAsync(); 
//                }
//            }
//            catch (Exception e) 
//            {
//                data.message = e.Message;
//                data.success = false;
//            }

//            return data;
//        }
//    }
//}