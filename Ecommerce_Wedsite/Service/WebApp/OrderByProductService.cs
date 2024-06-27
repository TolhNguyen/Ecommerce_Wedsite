using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IOrderByProductService // Tạo Interface
    {

        Task<ResponseMessageObject<OrderByProduct_ViewModel>> Service_Test(string? proname, int category, int orderby = 1, int page = 1); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class OrderByProductService : IOrderByProductService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public OrderByProductService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }
        public async Task<ResponseMessageObject<OrderByProduct_ViewModel>> Service_Test(string? proname, int category, int orderby, int page)
        {
            // Cho 1 trang max là 4 item
            var pageSize = 12;
            var data = new ResponseMessageObject<OrderByProduct_ViewModel>();
            data.Data = new OrderByProduct_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"SELECT * FROM Product where 1=1 and Product_Condition = 1 and Product_Quantity > 0");
                    if(proname != null)
                    {
                        query += $"and (Product_Name like {"%"+proname+"%"} or Product_ShortDescription like {"%" + proname + "%"})";
                        
                        // k cần N'.Cách viết đúng: like {"%"+proname+"%"}      
                    }
                    if(category != 0)
                    {
                        query += $"and (ProductType_Id = {category})";
                    }
                    switch (orderby) // case của orderby thôi
                    {
                        // Tạo switch case tùy theo giá trị nhận vào sẽ chạy query tương ứng (orderby và page từ nhận từ view và model)
                        case 1:
                            query += $"order by Product_Price desc";
                            break;
                        case 2:
                            query += $"order by Product_Price asc "; 
                            break;
                        case 3:
                            query += $"order by Product_Name desc ";
                            break;
                        case 4:
                            query += $"order by Product_Name asc ";
                            break;
                    }

                    query += $" OFFSET ({page} -1) * {pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

                    data.Data.orderbyproduct = await query.QueryAsync<Product>();     

                    await dbConn.CloseAsync(); // đóng sync sau khi sử dụng

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


