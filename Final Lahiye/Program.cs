using Final_Lahiye.Data;
using Final_Lahiye.DependencyInjection;
using Final_Lahiye.Membership;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultServer"));
});

builder.Services.AddDependecies();
//builder.Services.AddAutoMapper(typeof(MapperAdminPanelProfile));

builder.Services.AddIdentity<MUser, MRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(
    cfg =>
    {
        cfg.Password.RequireDigit = true;
        cfg.Password.RequireUppercase = true;
        cfg.Password.RequireLowercase = true;
        cfg.Password.RequireNonAlphanumeric = true;
        cfg.Password.RequiredLength = 8;
        cfg.User.RequireUniqueEmail = true;
        cfg.Lockout.MaxFailedAccessAttempts = 5;
        cfg.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0);
    });


builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.LoginPath = "/login.html";
    cfg.AccessDeniedPath = "/accessdenied/html";
    cfg.ExpireTimeSpan = new TimeSpan(0, 5, 0);
    cfg.Cookie.Name = "Emart";
});

builder.Services.AddScoped<UserManager<MUser>>();
builder.Services.AddScoped<SignInManager<MUser>>();

builder.Services.AddScoped<IClaimsTransformation, AppClaimProvider>();

builder.Services.AddControllers(
    cfg =>
    {
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        cfg.Filters.Add(new AuthorizeFilter(policy));
    });
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

/*app.MapAreaControllerRoute(
              name: "default",
              areaName: "Admin",
              pattern: "signin.html",
              defaults: new
              {
                  controller = "Account",
                  action = "Login",
                  area = "admin"
              });*/
app.Run();
