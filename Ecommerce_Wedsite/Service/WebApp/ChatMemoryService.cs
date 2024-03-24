
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
    public interface IChatMemoryService // Tạo Interface
    {
        Task<ResponseMessageObject<bool>> ChatMemoryFunction(string botmess, string usermess); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class ChatMemoryService : IChatMemoryService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public ChatMemoryService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }
        public async Task<ResponseMessageObject<bool>> ChatMemoryFunction(string botmess, string usermess) // tạo riêng 1 service. (bỏ static r)
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    //select: cùng id user thì insert chat vào db cùng id.

                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                return new ResponseMessageObject<bool> { success = false };
            }
            return new ResponseMessageObject<bool> { success = true };
        }
    }
}