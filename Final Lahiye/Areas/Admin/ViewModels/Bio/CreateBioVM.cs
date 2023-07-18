using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.HeaderUp;
public class CreateBioVM
{
    [Required]
    public string FacebookLink { get; set; }
    [Required]
    public string InstagramLink { get; set; }
    [Required]
    public string TwitterLink { get; set; }

    public string Text { get; set; }
}
