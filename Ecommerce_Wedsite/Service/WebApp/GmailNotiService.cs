using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Net.Mail;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IGmailNotiService // Tạo Interface
    {
        Task<ResponseMessageObject<bool>> GmailNoti(string? gmail); // cách trả vê là true or false (còn hay k còn sl discount)
    }
    public class GmailNotiService : IGmailNotiService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public GmailNotiService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        // có thể sử dụng chung viewmodel đc
        public async Task<ResponseMessageObject<bool>> GmailNoti(string? gmail) // Chức năng: lấy random id lên ss và tính số lượng
        {
            try
            {
                

            }
            catch (Exception e)
            {
                return new ResponseMessageObject<bool> { success = false }; //false = hết => k discount được
            }
            return new ResponseMessageObject<bool> { success = true }; // true = còn => giảm discount thành công
        }
    }
}

