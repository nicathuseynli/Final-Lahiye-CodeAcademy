namespace Final_Lahiye.Models;
public class FaqPage : BaseEntity<int>
{
    public string Title { get; set; }

    public string Question { get; set; }

    public string Answer { get; set; }
}
