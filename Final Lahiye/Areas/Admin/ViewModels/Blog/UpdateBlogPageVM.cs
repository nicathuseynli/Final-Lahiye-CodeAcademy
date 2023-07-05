using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Blog;
public class UpdateBlogPageVM
{
    public int Id { get; set; }

    public int AuthorId { get; set; }

    public string Title { get; set; }

    public string AuthorName { get; set; }

    [AllowNull]
    public string Image { get; set; }

    public int BlogId { get; set; }

    public IFormFile Photo { get; set; }
}
