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
    public interface INewsAdminEditFunctionService // Tạo Interface
    {
        Task<ResponseMessageObject<string>> Service_Test(News newsitem); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class NewsAdminEditFunctionService : INewsAdminEditFunctionService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public NewsAdminEditFunctionService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<string>> Service_Test(News newsitem) // Lấy dữ liệu model từ db lên và hành động vào.
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select top 1 * from News where News_Id = {newsitem.News_Id}"); // select theo top id
                    var news = await query.QueryFirstOrDefaultAsync<News>(); /// lấy du lieu tu db. 

                    //tạo biến lưu dữ liệu mới vào. Dùng QueryFODA để lấy những column duy nhất ra.

                    //  map du lieu db  cli
                    //  mapper 
                    // map tay 

                    //header.Header_Title = headeritem.Header_Title;

                    news.News_Id = newsitem.News_Id;
                    news.News_Title = newsitem.News_Title;
                    news.News_SubTitle = newsitem.News_SubTitle;
                    news.Author = newsitem.Author;
                    news.Picture_Id = newsitem.Picture_Id;
                    news.ListNews_Id = newsitem.ListNews_Id;
                    news.News_Content = newsitem.News_Content;
                    news.News_DatePost = newsitem.News_DatePost;

                    var it = dbConn.Update(news);

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



