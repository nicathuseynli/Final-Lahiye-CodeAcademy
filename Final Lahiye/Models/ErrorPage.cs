using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Lahiye.Models;
public class ErrorPage:BaseEntity<int>
{
    public string Message { get; set; }

    public string Image { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }
}
