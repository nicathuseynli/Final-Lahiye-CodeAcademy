using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.Blog;
public class CreateAuthorVM
{
    [Required]
    public string FullName { get; set; }

    [Required]
    public int  Blogid { get; set; }
    [Required]
    public string Proffession { get; set; }

    public IFormFile Photo { get; set; }
}
