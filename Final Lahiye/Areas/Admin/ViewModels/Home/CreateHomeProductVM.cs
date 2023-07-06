using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class CreateHomeProductVM
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }

    public int SalePercent { get; set; }

    public decimal LastPrice { get; set; }

    [Required]
    public decimal CurrentPrice { get; set; }

    public int CategoryId { get; set; }

    public int ColourId { get; set; }

    public int StockStatusId { get; set; }

    [Required]
    public IFormFile Photo { get; set; }

}
