using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Blog;
public class UpdateBlogPageVM
{
    public int Id { get; set; }

    public int AuthorId { get; set; }

    public string Title { get; set; }

    public string HeaderUp { get; set; }

    public string InformationUp { get; set; }

    public string HeaderMiddle { get; set; }

    public string InformationMiddle { get; set; }

    public string MiddleTextFirst { get; set; } 

    public string MiddleTextSecond { get; set; } 

    public string MiddleTextThird { get; set; } 

    public string MiddleTextFourth { get; set; } 

    public string MiddleDescription { get; set; } 

    public string HeaderEnd { get; set; } 

    public string InformationEnd { get; set; } 

    public string ServiceInfo { get; set; } 

    public string ServiceNameFirst { get; set; } 

    public int ServicePriceFirst { get; set; }

    public string ServiceDescriptionFirst { get; set; } 

    public string ServiceNameSecond { get; set; } 

    public int ServicePriceSecond { get; set; }

    public string ServiceDescriptionSecond { get; set; } 

    public string ServiceNameThird { get; set; }

    public int ServicePriceThird { get; set; }

    public string ServiceDescriptionThird { get; set; } 

    public string ServiceNameFourth { get; set; } 

    public int ServicePriceFourth { get; set; }

    public string ServiceDescriptionFourth { get; set; } 

    public string ApplyInfo { get; set; }

    public string ApplyInfoLeftImage { get; set; }

    public string ApplyInfoRightImage { get; set; }

    public string Image { get; set; }

    [AllowNull]
    public IFormFile Photo { get; set; }

    [AllowNull]
    public IFormFile ApplyInfoLeftPhoto { get; set; }

    [AllowNull]
    public IFormFile ApplyInfoRightPhoto { get; set; }
}

  


