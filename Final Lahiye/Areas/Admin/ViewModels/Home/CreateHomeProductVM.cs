using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class CreateHomeProductVM
{
    [Required]
    public string Name { get; set; }

    public int SalePercent { get; set; }

    public decimal LastPrice { get; set; }

    public string Header { get; set; }

    [Required]
    public bool InStock { get; set; }

    [Required]
    public decimal CurrentPrice { get; set; }

    public int CategoryId { get; set; }

    public int ColourId { get; set; }

    [Required]
    public IFormFile Photo { get; set; }

}
