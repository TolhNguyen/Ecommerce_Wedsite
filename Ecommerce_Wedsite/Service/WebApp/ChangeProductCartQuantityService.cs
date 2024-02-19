﻿using DapperQueryBuilder;
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
	public interface IChangeProductCartQuantityService // Tạo Interface
	{
		ShopCard_ViewModel ChangeProductCartQuantityFunction(ShopCard_ViewModel card, int id, int qty); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
	}
	public class ChangeProductCartQuantityService : IChangeProductCartQuantityService // Thừa kế các thuộc tính từ Interface 
	{
		private readonly IConfigManagerService _configuration;
		public ChangeProductCartQuantityService(IConfigManagerService configuration)
		{
			_configuration = configuration;
		}
		public ShopCard_ViewModel ChangeProductCartQuantityFunction(ShopCard_ViewModel card, int id, int qty) // tạo riêng 1 service. (bỏ static r)
		{
			foreach (var item in card.product_card) // product_card là object chứa các sp trong giỏ hàng (card là obj lớn)
			{
				if (id == item.id) // chỉ lấy sp có id đó ra từ giỏ hàng
				{
					if(item.qty != qty)
					{
						card.total_price -= item.price;
						item.price /= item.qty; // trừ sl cũ
						item.qty = qty; // sl mới
						item.price *= item.qty;
						card.total_price += item.price;
						break;
					}
				}
			}
			return card;
		}
	}
}
