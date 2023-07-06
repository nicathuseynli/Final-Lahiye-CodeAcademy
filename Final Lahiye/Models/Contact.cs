using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Lahiye.Models;
public class Contact : BaseEntity<int>
{
    public string City { get; set; }
    public string Magazine { get; set; }
    public string Description { get; set; }

    public string Image { get; set; }
    [NotMapped]
    public IFormFile Photo { get; set; }
}
