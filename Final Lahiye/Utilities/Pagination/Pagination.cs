using Final_Lahiye.Models;

namespace Final_Lahiye.Utilities.Pagination;
public class Pagination<T>: BaseEntity<int>
{
    public List<T> Products { get; set; }

    public int CuurentPage { get; set; }

    public int TotalPage { get; set; }

    public Pagination(List<T> products, int currentPage, int totalPage)
    {
        Products = products;
        CuurentPage = currentPage;
        TotalPage = totalPage;
    }

    //nav
    public int HomeProductId { get; set; }
    public HomeProduct Product { get; set; }

}

