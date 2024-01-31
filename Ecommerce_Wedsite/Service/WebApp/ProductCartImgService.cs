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
    public interface IProductCartImgService // Tạo Interface
    {
        Task<string> ProductCartImgFunction(int id); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class ProductCartImgService : IProductCartImgService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public ProductCartImgService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> ProductCartImgFunction(int id) // tạo riêng 1 service. (bỏ static r)
        {
            var picturefilename = "";
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Picture where Picture_Id = {id}");

                    var picture = await query.QueryFirstOrDefaultAsync<Picture>();

                    picturefilename = picture.Picture_FileName;

                     // tính số lượng sp mới


                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                return "";
            }
            return picturefilename;
        }
    }
}

