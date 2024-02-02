using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Models.Entities
{
	[Table("Video")]
	public class Video
	{
		[Key]
		public int Video_Id { get; set; }
		public string Video_Title { get; set; }
		public string Video_FileName { get; set; }
	}

}
