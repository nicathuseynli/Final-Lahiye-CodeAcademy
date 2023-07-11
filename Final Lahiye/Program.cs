using Final_Lahiye.Areas.Admin.Services.Implementations;
using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Services.Implementations;
using Final_Lahiye.Areas.Services.Interface;
using Final_Lahiye.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultServer"));
});

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactDetailsService, ContactDetailsService>();
builder.Services.AddScoped<IElementorService, ElementorService>();
builder.Services.AddScoped<ITestimonialService, TestimonialService>();
builder.Services.AddScoped<IShortInfoService, ShortInfoService>();
builder.Services.AddScoped<IRegisterPageService, RegisterPageService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ILoginPageService,LoginPageService>();
builder.Services.AddScoped<IRegisterPageService,RegisterPageService>();
builder.Services.AddScoped<IHeroService,HeroService>();
builder.Services.AddScoped<IHeaderUpTextService,HeaderUpTextService>();
builder.Services.AddScoped<IHeaderUpSocialMediaService,HeaderUpSocialMediaService>();
builder.Services.AddScoped<IFaqService,FaqService>();
builder.Services.AddScoped<IErrorService,ErrorService>();
builder.Services.AddScoped<IColourService,ColourService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
