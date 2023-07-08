using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class UpdateHomeProductVM
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Header { get; set; }

    public int SalePercent { get; set; }

    public decimal LastPrice { get; set; }

    public bool InStock { get; set; }

    public decimal CurrentPrice { get; set; }

    public int CategoryId { get; set; }

    public int ColourId { get; set; }

    [AllowNull]
    public IFormFile Photo { get; set; }

    public string Image { get; set; }
}
