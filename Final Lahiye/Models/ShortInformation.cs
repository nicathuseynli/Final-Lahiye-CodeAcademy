using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Lahiye.Models;
public class ShortInformation : BaseEntity<int>
{
    public string Title { get; set; }

    public string Icon { get; set; }

    [NotMapped]
    public IFormFile IconPhoto { get; set; }

    public string Information { get; set; }
}
