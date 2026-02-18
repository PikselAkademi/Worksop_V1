using System.ComponentModel.DataAnnotations;

namespace V1.Web.Areas.Admin.Models;

public class PageContentEditViewModel
{
    public int Id { get; set; }

    [Required]
    public string Slug { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Sayfa Baþlýðý")]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Ýçerik")]
    [MaxLength(5000)]
    public string Body { get; set; } = string.Empty;

    [Display(Name = "Güncelleyen")]
    [MaxLength(80)]
    public string UpdatedBy { get; set; } = "Admin";
}
