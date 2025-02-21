﻿using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IPictureService // Tạo Interface
    {
        // Dùng 1 model nhỏ HVM để Service chạy lệnh query của Header
        //RMObject chỉ lấy từng 1 dữ liệu ( vd là model có từng danh sách 1 như Header_ViewModel)
        // Sẽ là RM khi lấy toàn bộ dữ liệu cùng lúc (vd là model có nhiều dữ liệu đi chung như Header)
        Task<ResponseMessageObject<Picture_ViewModel>> Service_Test(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class PictureService : IPictureService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public PictureService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<Picture_ViewModel>> Service_Test() // Code cho Phương Thức Service
        {
            var data = new ResponseMessageObject<Picture_ViewModel>(); // Tạo biến dữ liệu. data là biến lớn của RMO
            data.Data = new Picture_ViewModel(); // Tạo biến lưu trữ để Data Không lỗi báo rỗng. Data là biến nhỏ hơn là HVM. (Lỗi cơ bản)
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Picture"); // thao tác querry 
                    var query2 = dbConn.QueryBuilder($"select * from Picture where Picture_Level = 2"); // lấy list con lên. Nếu lv3 thì lấy riêng thêm = 3
                    //data.Data = await query.QueryAsync<AllLayout>(); // lưu vào dữ liệu
                    data.Data.subpicture = await query2.QueryAsync<Picture>();
                    data.Data.picture = await query.QueryAsync<Picture>(); // chỉ lấy mỗi 1 header_Lv1 nên là dạng Object
                    // Vào biến lớn "data." đến biến nhỏ hơn "Data." sau đó vào danh sách nhỏ nữa là "header_VL1(trong model header_ViewModel"


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
