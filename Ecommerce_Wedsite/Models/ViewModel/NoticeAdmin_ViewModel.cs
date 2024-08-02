using Ecommerce_Wedsite.Models.ViewModel;

public class NoticeAdmin_ViewModel
{
    public List<NoticeAdmin> notice { get; set; } = new List<NoticeAdmin>(); // thành 1 list
    
}

public class NoticeAdmin
{
    public int id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Content { get; set; }
    public string Link { get; set; }
    public DateTime Date { get; set; }
}
