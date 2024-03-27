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
    public interface IHomePageAdminEditFunctionService // Tạo Interface
    {
        Task<ResponseMessageObject<string>> EditFunction(HomePage homepageitem); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class HomePageAdminEditFunctionService : IHomePageAdminEditFunctionService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public HomePageAdminEditFunctionService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<string>> EditFunction(HomePage homepageitem) // Lấy dữ liệu model từ db lên và hành động vào.
        {
            //var data = new ResponseMessageObject<Admin_ViewModel>();
            //data.Data. = new Header_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select top 1 * from HomePage where HomePage_Id = {homepageitem.HomePage_Id}"); // select theo top id
                    var homepage = await query.QueryFirstOrDefaultAsync<HomePage>(); /// du lieu tu db. tạo biến lưu dữ liệu mới vào. Dùng QueryFODA để lấy những column duy nhất ra.

                    //  map du lieu db  cli
                    //  mapper 
                    // map tay 

                    homepage.HomePage_Title = homepageitem.HomePage_Title;
                    homepage.HomePage_Condition = homepageitem.HomePage_Condition;
                    homepage.HomePageParent_Id = homepageitem.HomePageParent_Id;
                    homepage.HomePage_Levels = homepageitem.HomePage_Levels;
                    homepage.HomePage_Type = homepageitem.HomePage_Type;
                    homepage.Logo_Id = homepageitem.Logo_Id;
                    homepage.PictureGrp_Id = homepageitem.PictureGrp_Id;
                    homepage.HTML_Id = homepageitem.HTML_Id;
                    homepage.Video_Id = homepageitem.Video_Id;
                    homepage.HomePage_Img = homepageitem.HomePage_Img;
                    homepage.HomePage_Summary = homepageitem.HomePage_Summary;
                    homepage.HomePage_Order = homepageitem.HomePage_Order;
                    homepage.HomePage_Video = homepageitem.HomePage_Video;
                    homepage.HomePage_ProductDisplayId = homepageitem.HomePage_ProductDisplayId;

                    var it = dbConn.Update(homepage);

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


