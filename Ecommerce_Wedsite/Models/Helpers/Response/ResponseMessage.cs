namespace Ecommerce_Wedsite.Models.Helpers.Response
{
    public class ResponseMessage<T> // Truyền list (nhieu du lieu). vd dùng khi: nhiều người 1 vật chất (chung)
    {
        public IEnumerable<T>? Data { get; set; }
        public T Item { get; set; }
        public T Data_total { get; set; }
        public byte[] DataByte { get; set; }
        public int Total { get; set; }
        public int id { get; set; }
        /// <summary> Trạng thái thành công hay thất bại </summary>
        public bool success { get; set; } = true;
        /// <summary> Lỗi trả về khi thất bại </summary>
        public string message { get; set; }
        public int statusCode { get; set; } = 200;
    }
}
