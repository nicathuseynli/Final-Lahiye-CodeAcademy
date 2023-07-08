using Final_Lahiye.Models;

namespace Final_Lahiye.ViewModels;
public class HomeVM
{
    public Hero Hero { get; set; }

    public Banner Banner { get; set; }

    public Elementor Elementor { get; set; }

    public HomeProduct Product { get; set; }

    public Testimonial Testimonial { get; set; }

    public List<ShortInformation> ShortInformations { get; set; }

    public List<Testimonial> Testimonials { get; set; }

    public List<HomeProduct> Products { get; set; }
}
