using AutoMapper;
using Final_Lahiye.Areas.Admin.ViewModels.Blog;
using Final_Lahiye.Areas.Admin.ViewModels.Contact;
using Final_Lahiye.Areas.Admin.ViewModels.Error;
using Final_Lahiye.Areas.Admin.ViewModels.FAQ;
using Final_Lahiye.Areas.Admin.ViewModels.HeaderUp;
using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Areas.Admin.ViewModels.LoginPage;
using Final_Lahiye.Areas.Admin.ViewModels.RegisterPage;
using Final_Lahiye.Areas.Admin.ViewModels.ShopPage;
using Final_Lahiye.Models;

namespace Final_Lahiye.Areas.Admin.Mappers;
public class MapperAdminPanelProfile : Profile
{
    public MapperAdminPanelProfile()
    {
        CreateMap<Blog,CreateBlogPageVM>();
        CreateMap<Blog, UpdateBlogPageVM>().ReverseMap();
        CreateMap<Author,CreateAuthorVM>();
        CreateMap<Author,UpdateAuthorVM>().ReverseMap();
        CreateMap<Contact, CreateContactVM>();
        CreateMap<Contact, UpdateContactVM>().ReverseMap();
        CreateMap<ContactDetails, CreateContactDetailsVM>();
        CreateMap<ContactDetails,UpdateContactDetailsVM>().ReverseMap();
        CreateMap<ErrorPage, CreateErrorPageVM>();
        CreateMap<ErrorPage,UpdateErrorPageVM>().ReverseMap();
        CreateMap<FaqPage,CreateFaqPageVM>();
        CreateMap<FaqPage,UpdateFaqPageVM>().ReverseMap();
        CreateMap<HeaderUpText,CreateHeaderUpTextVM>();
        CreateMap<HeaderUpText,UpdateHeaderUpTextVM>().ReverseMap();
        CreateMap<HeaderUpSocialMedia, CreateHeaderUpSocialMediaVM>();
        CreateMap<HeaderUpSocialMedia,UpdateHeaderUpSocialMediaVM>().ReverseMap();
        CreateMap<Banner,CreateBannerVM>();
        CreateMap<Banner,UpdateBannerVM>().ReverseMap();
        CreateMap<Elementor,CreateElementorWidgetVM>();
        CreateMap<Elementor, UpdateElementorWidgetVM>().ReverseMap();
        CreateMap<HomeProduct,CreateHomeProductVM>();
        CreateMap<HomeProduct,UpdateHomeProductVM>().ReverseMap();
        CreateMap<ShortInformation,CreateShortInfoVM>();
        CreateMap<ShortInformation, UpdateShortInfoVM>().ReverseMap();
        CreateMap <LoginPage,CreateLoginPageVM>();
        CreateMap <LoginPage,UpdateLoginPageVM>().ReverseMap();
        CreateMap <RegisterPage,CreateRegisterPageVM>();
        CreateMap <RegisterPage, UpdateRegisterPageVM>().ReverseMap();
        CreateMap <Category, CreateCategoryVM>();
        CreateMap <Category, UpdateCategoryVM>().ReverseMap();
        CreateMap<Colour, CreateColourVM>();
        CreateMap <Colour, UpdateColourVM>().ReverseMap();
    }
}
