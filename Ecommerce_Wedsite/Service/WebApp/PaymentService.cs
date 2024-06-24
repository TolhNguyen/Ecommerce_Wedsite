using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IPaymentService // Tạo Interface
    {
        // Dùng 1 model nhỏ HVM để Service chạy lệnh query của Header
        //RMObject chỉ lấy từng 1 dữ liệu ( vd là model có từng danh sách 1 như Header_ViewModel)
        // Sẽ là RM khi lấy toàn bộ dữ liệu cùng lúc (vd là model có nhiều dữ liệu đi chung như Header)
        Task<ResponseMessageObject<Payment_ViewModel>> Service_Test(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class PaymentService : IPaymentService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public PaymentService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<Payment_ViewModel>> Service_Test() 
        {
            var data = new ResponseMessageObject<Payment_ViewModel>(); 
            data.Data = new Payment_ViewModel(); 
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) 
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Payment where Payment_Condition = 1"); 
                    data.Data.payment = await query.QueryAsync<Payment>();

                    await dbConn.CloseAsync(); 
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

