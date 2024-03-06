using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Net;
using System.Net.Mail;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IUserEmailService // Tạo Interface
    {
        Task<ResponseMessageObject<bool>> Email(string checkemail); 
    }
    public class UserEmailService : IUserEmailService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public UserEmailService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }
        public async Task<ResponseMessageObject<bool>> Email(string checkemail) // Lỗi chưa dùng được gì chưa đăng nhập gmail đc
        {
            try
            {
                var subject = "Thông báo Email đăng ký website thành công";
                var body = "Chúc mừng đã đăng ký thành viên thành công, bạn đã được nhận discount giảm giá!!!"; // có thể viết dạng html, tùy
                var smtpClient = new SmtpClient("smtp.gmail.com") // Máy chủ SMTP của Gmail
                {
                    Port = 587, // Port sử dụng cho TLS
                    Credentials = new NetworkCredential("beenquan15@gmail.com", "Beenquan1509"), // Thay thế bằng email và mật khẩu của bạn
                    EnableSsl = true, // Kích hoạt SSL
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("beenquan15@gmail.com"), // Email người gửi phải giống với email đã nhập ở trên
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true, // Đặt là true nếu body có chứa HTML
                };
                mailMessage.To.Add(checkemail);

                smtpClient.Send(mailMessage);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception caught in CreateTestMessage2(): {ex}"); // in ra lỗi là gì
                return new ResponseMessageObject<bool> { success = false };
            }
            return new ResponseMessageObject<bool> { success = true };
        }
    }
}
