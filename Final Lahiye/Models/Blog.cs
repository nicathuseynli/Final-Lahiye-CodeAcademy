using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Lahiye.Models;
public class Blog :BaseEntity<int>
{
    public string Title { get; set; } = string.Empty;

    public string HeaderUp { get; set; } = string.Empty;

    public string InformationUp { get; set; } = string.Empty;

    public string HeaderMiddle { get; set; } = string.Empty;

    public string InformationMiddle { get; set; } = string.Empty;

    public string MiddleTextFirst { get; set; } = string.Empty;

    public string MiddleTextSecond { get; set; } = string.Empty;

    public string MiddleTextThird { get; set; } = string.Empty;

    public string MiddleTextFourth { get; set; } = string.Empty;

    public string MiddleDescription { get; set; } = string.Empty;

    public string HeaderEnd { get; set; } = string.Empty;

    public string InformationEnd { get; set; } = string.Empty;

    public string ServiceInfo { get; set; } = string.Empty;

    public string ServiceNameFirst { get; set; } = string.Empty;

    public int ServicePriceFirst { get; set; }

    public string ServiceDescriptionFirst { get; set; } = string.Empty;
    
    public string ServiceNameSecond { get; set; } = string.Empty;

    public int ServicePriceSecond { get; set; }

    public string ServiceDescriptionSecond { get; set; } = string.Empty;
    
    public string ServiceNameThird { get; set; } = string.Empty;

    public int ServicePriceThird { get; set; }

    public string ServiceDescriptionThird { get; set; } = string.Empty;
    
    public string ServiceNameFourth { get; set; } = string.Empty;

    public int ServicePriceFourth { get; set; }

    public string ServiceDescriptionFourth { get; set; } = string.Empty;

    public string ApplyInfo { get; set; } = string.Empty;

    public string ApplyInfoLeftImage { get; set; } = string.Empty;

    public string ApplyInfoRightImage { get; set; } = string.Empty;


    [NotMapped]
    public IFormFile ApplyInfoLeftPhoto { get; set; }

    [NotMapped]
    public IFormFile ApplyInfoRightPhoto { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }

    public string Image { get; set; } = string.Empty;

    //nav
    public int AuthorId { get; set; }

    public Author? Author { get; set; }
}
