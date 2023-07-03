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
}
