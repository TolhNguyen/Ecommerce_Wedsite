
using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface INoticeAdminService // Tạo Interface
    {
        Task<NoticeAdmin_ViewModel> NoticeAdminFunction(NoticeAdmin_ViewModel notiVM, string content, int id); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
        Task<NoticeAdmin_ViewModel> NoticeAdminFunctionFake(NoticeAdmin_ViewModel notiVM, int id, int dem);
    }
    public class NoticeAdminService : INoticeAdminService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public NoticeAdminService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<NoticeAdmin_ViewModel> NoticeAdminFunction(NoticeAdmin_ViewModel notiVM, string content, int id)
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync();
                    var query = dbConn.QueryBuilder($"select * from Admin where Admin_Id = '{id}'");
                    var n = await query.QueryFirstOrDefaultAsync<Admin>();

                    var noti = new NoticeAdmin();
                    noti.id = id;
                    noti.Name = n.Admin_Name;
                    noti.Content = content;
                    noti.Date = DateTime.Now;
                    notiVM.notice.Add(noti);

                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e) // Gặp lỗi
            {
                return notiVM;
            }
            return notiVM;
        }

        public async Task<NoticeAdmin_ViewModel> NoticeAdminFunctionFake(NoticeAdmin_ViewModel notiVM, int id, int dem)
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync();
                    var query = dbConn.QueryBuilder($"select * from Admin where Admin_Id = '{id}'");
                    var n = await query.QueryFirstOrDefaultAsync<Admin>();

                    var noti = new NoticeAdmin();
                    noti.id = id;
                    noti.Name = n.Admin_Name;
                    noti.Date = DateTime.Now;

                    if (dem == 0)
                    {
                        noti.Content = n.Admin_Name + " đã đăng nhập";
                    }
                    else
                    {
                        noti.Content = n.Admin_Name + " đã đăng nhập lần thứ " + dem;
                    }

                    notiVM.notice.Add(noti); // Lưu vào model để hiển thị
                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e) // Gặp lỗi
            {
                return notiVM;
            }
            return notiVM;
        }
    }
}
