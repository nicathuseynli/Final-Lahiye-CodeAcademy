using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Lahiye.Models;
public class Author : BaseEntity<int>
{
    public string FullName { get; set; }

    public string Proffesion { get; set; }

    public string Image { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }

    //navigation
    public virtual ICollection<Blog> Blogs { get; set; }
}
