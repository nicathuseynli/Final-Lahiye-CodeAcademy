using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.FAQ;
public class CreateFaqPageVM
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Question { get; set; }
    
    [Required]
    public string Answer { get; set; }
}
