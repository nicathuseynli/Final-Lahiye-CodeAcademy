using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.Blog;
public class CreateBlogPageVM
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string AuthorName { get; set; }

    public int AuthorId { get; set; }

    [Required]
    public IFormFile Photo { get; set; }
}
