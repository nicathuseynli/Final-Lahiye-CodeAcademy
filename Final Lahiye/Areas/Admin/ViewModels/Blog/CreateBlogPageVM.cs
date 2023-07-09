using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.Blog;
public class CreateBlogPageVM
{
    [Required]
    public string Title { get; set; }

    public int AuthorId { get; set; }

    [Required]
    public string HeaderUp { get; set; }

    [Required]
    public string InformationUp { get; set; }

    [Required]
    public string HeaderMiddle { get; set; }

    [Required]
    public string InformationMiddle { get; set; }

    [Required]
    public string HeaderEnd { get; set; }

    [Required]
    public string InformationEnd { get; set; }

    [Required]
    public string ServiceInfo { get; set; }

    [Required]
    public string MiddleTextFirst { get; set; }

    [Required]
    public string MiddleTextSecond { get; set; }

    [Required]
    public string MiddleTextThird { get; set; }

    [Required]
    public string MiddleTextFourth { get; set; }

    [Required]
    public string MiddleDescription { get; set; }

    [Required]
    public string ServiceNameFirst { get; set; }

    [Required]
    public int ServicePriceFirst { get; set; }

    [Required]
    public string ServiceDescriptionFirst { get; set; }

    [Required]
    public string ServiceNameSecond { get; set; }

    [Required]
    public int ServicePriceSecond { get; set; }

    [Required]
    public string ServiceDescriptionSecond { get; set; }

    [Required]
    public string ServiceNameThird { get; set; }

    [Required]
    public int ServicePriceThird { get; set; }

    [Required]
    public string ServiceDescriptionThird { get; set; }

    [Required]
    public string ServiceNameFourth { get; set; } 

    [Required]
    public int ServicePriceFourth { get; set; }

    [Required]
    public string ServiceDescriptionFourth { get; set; } 

    [Required]
    public string ApplyInfo { get; set; }

    [Required]
    public IFormFile ApplyInfoLeftPhoto { get; set; }

    [Required]
    public IFormFile ApplyInfoRightPhoto { get; set; }

    [Required]
    public IFormFile Photo { get; set; }
}
