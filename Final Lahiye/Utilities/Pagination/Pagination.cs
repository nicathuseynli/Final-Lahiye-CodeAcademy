//using Final_Lahiye.Models;

//namespace Final_Lahiye.Utilities.Pagination;
//public class Pagination<T>: BaseEntity<int>
//{
//    public List<T> Products { get; set; }

//    public int CuurentPage { get; set; }

//    public int TotalPage { get; set; }

//    public Pagination(List<T> products, int currentPage, int totalPage)
//    {
//        Products = products;
//        CuurentPage = currentPage;
//        TotalPage = totalPage;
//    }
//    //nav
//    public int HomeProductId { get; set; }
//    public HomeProduct Product { get; set; }

//}
//namespace Final_Lahiye.Models;
//public class Paginate<T> : BaseEntity<int>
//{
//    public List<T> Products { get; set; }
//    public int CurrentPage { get; set; }
//    public int TotalPage { get; set; }

//    // Foreign Key
//    public int HomeProductId { get; set; }

//    public Paginate(List<T> products, int currentPage, int totalPage)
//    {
//        Products = products;
//        CurrentPage = currentPage;
//        TotalPage = totalPage;
//    }

//    // Navigation Property
//    public HomeProduct HomeProduct { get; set; }
//}
