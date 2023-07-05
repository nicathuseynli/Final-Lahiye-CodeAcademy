using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Lahiye.Models;
public class Blog :BaseEntity<int>
{
    public string Title { get; set; }

    public string AuthorName { get; set; }

    public string Image { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }

    //nav
    public int AuthorId { get; set; }

    public Author Author { get; set; }
}
