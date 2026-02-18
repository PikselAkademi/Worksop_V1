namespace V1.Web.Models;

public class ContentPageViewModel
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime UpdatedAtUtc { get; set; }
}
