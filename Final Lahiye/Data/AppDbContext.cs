using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Data;
public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<HeaderUpSocialMedia> HeaderUpSocialMedias { get; set; }
    public DbSet<HeaderUpText> HeaderUpTexts { get; set; }
    public DbSet<Hero> Heros { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Elementor> Elementors { get; set; }
    public DbSet<Testimonial> Testimonials { get; set; }
    public DbSet<ShortInformation> ShortInformations { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactDetails> ContactDetailss { get; set; }
    public DbSet<LoginPage> LoginPages { get; set; }
    public DbSet<RegisterPage> RegisterPages { get; set; }
    public DbSet<ErrorPage> ErrorPages { get; set; }
    public DbSet<FaqPage> FaqPages { get; set; }
}
