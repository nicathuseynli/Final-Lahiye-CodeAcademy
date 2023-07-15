using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.FAQ;
public class CreateFaqPaymentVM
{
    [Required]
    public string Question { get; set; }

    [Required]
    public string Answer { get; set; }
}
