namespace Ecommerce_Wedsite.Models.Helpers.Response
{
    public class ResponseMessageObject<T>
    {
        public T Data { get; set; }
        public int Total { get; set; }
        /// <summary>
        /// Trạng thái thành công hay thất bại
        /// </summary>
        public bool success { get; set; } = true;
        /// <summary>
        /// Lỗi trả về khi thất bại
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// Mã trạng thái
        /// </summary>
        public int statusCode { get; set; }
    }
}
