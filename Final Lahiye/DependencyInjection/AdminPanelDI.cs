using Final_Lahiye.Areas.Admin.Services.Implementations;
using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Services.Implementations;
using Final_Lahiye.Areas.Services.Interface;

namespace Final_Lahiye.DependencyInjection;
public static class AdminPanelDI
{
    public static IServiceCollection AddDependecies(this IServiceCollection services)
    {
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<IBannerService, BannerService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IContactDetailsService, ContactDetailsService>();
        services.AddScoped<IElementorService, ElementorService>();
        services.AddScoped<ITestimonialService, TestimonialService>();
        services.AddScoped<IShortInfoService, ShortInfoService>();
        services.AddScoped<IRegisterPageService, RegisterPageService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ILoginPageService, LoginPageService>();
        services.AddScoped<IRegisterPageService, RegisterPageService>();
        services.AddScoped<IHeroService, HeroService>();
        services.AddScoped<IHeaderUpTextService, HeaderUpTextService>();
        services.AddScoped<IHeaderUpSocialMediaService, HeaderUpSocialMediaService>();
        services.AddScoped<IFaqService, FaqService>();
        services.AddScoped<IFaqPaymentService, FaqPaymentService>();
        services.AddScoped<IColourService, ColourService>();
        return services;
    }
}
