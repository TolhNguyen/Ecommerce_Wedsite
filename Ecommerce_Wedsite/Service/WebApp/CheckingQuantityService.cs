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
	public interface ICheckingQuantityService // Tạo Interface
	{
		Task<ResponseMessageObject<bool>> CheckingQuantityFunction(ShopCard_ViewModel cartcookieobj); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
	}
	public class CheckingQuantityService : ICheckingQuantityService // Thừa kế các thuộc tính từ Interface 
	{
		private readonly IConfigManagerService _configuration;
		public CheckingQuantityService(IConfigManagerService configuration)
		{
			_configuration = configuration;
		}
		public async Task<ResponseMessageObject<bool>> CheckingQuantityFunction(ShopCard_ViewModel cartcookieobj) // tạo riêng 1 service. (bỏ static r)
		{
			try
			{
				using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
				{
					await dbConn.OpenAsync(); // mở sync

					var productobj = cartcookieobj.product_card;
					foreach (Producd_ShopCard item in productobj) //lấy từng dữ liệu ra. vòng lặp cách 2: foreach (var/Product_ShopCart item in card.product_card)
					{
						var query = dbConn.QueryBuilder($"select top 1 * from Product where Product_Id = '{item.id}'");
						var qtycheck = await query.QueryFirstOrDefaultAsync<Product>();
						if(qtycheck.Product_Quantity > item.qty)
						{
							new ResponseMessageObject<bool> { success = true };
						}
						else
						{
							return new ResponseMessageObject<bool> { success = false, message = $"Sản phẩm '{item.name}' vừa hết, vui lòng chọn lại số lượng!" };
							// chưa nghĩ ra cách + dồn đoạn responemessage này.
						}
					}

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

