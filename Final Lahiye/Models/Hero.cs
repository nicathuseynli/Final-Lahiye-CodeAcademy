namespace Final_Lahiye.Models;
public class Hero :BaseEntity<int>
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string HeroImage { get; set; }
}
