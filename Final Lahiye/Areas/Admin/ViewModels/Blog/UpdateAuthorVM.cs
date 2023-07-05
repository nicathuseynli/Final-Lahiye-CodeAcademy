using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Blog;
public class UpdateAuthorVM
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string Proffession { get; set; }
    public int Blogid { get; set; }


    [AllowNull]
    public string Image { get; set; }

    public IFormFile Photo { get; set; }

}
