using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Contact;
public class UpdateContactVM
{
    public int Id { get; set; }

    public string ByAddress { get; set; }

    public string ByEmail { get; set; }

    public string ByPhone { get; set; }

    public string City { get; set; }

    public string Magazine { get; set; }

    public string Description { get; set; }

    [AllowNull]
    public string Image { get; set; }

    public IFormFile Photo { get; set; }
}
