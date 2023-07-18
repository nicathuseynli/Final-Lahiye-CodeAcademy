namespace Final_Lahiye.Models;
public class Paginate<T>
{
    public List<T> HomeProducts { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }

    public Paginate(List<T> homeProducts, int currentPage, int totalPage)
    {
        HomeProducts = homeProducts;
        CurrentPage = currentPage;
        TotalPage = totalPage;
    }
}
