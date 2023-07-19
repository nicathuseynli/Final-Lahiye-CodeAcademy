using Final_Lahiye.Membership;
using Final_Lahiye.Models;
using Final_Lahiye.Models.Membership;
//using Final_Lahiye.Utilities.Pagination;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Data;
public class AppDbContext : IdentityDbContext<MUser,MRole,int,MUserClaim,MUserRole,MUserLogin,MRoleClaim,MUserToken>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Bio> Bios { get; set; }
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
    public DbSet<FaqPayment> FaqPaymentPages { get; set; }
    public DbSet<HomeProduct> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Colour> Colours { get; set; }
    public DbSet<Comment> Comments { get; set; }
    //public DbSet<Pagination<HomeProduct>> Paginations { get; set; }

    public DbSet<MUser> Users { get; set; }
    public DbSet<MRole> Roles { get; set; }
    public DbSet<MUserClaim> UserClaims { get; set; }
    public DbSet<MRoleClaim> RoleClaims { get; set; }
    public DbSet<MUserToken> UserTokens { get; set; }
    public DbSet<MUserLogin> UserLogins { get; set; }
    public DbSet<MUserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<MUser>(e =>
        {
            e.ToTable("Roles", "MemberShip");
        });
        builder.Entity<MRoleClaim>(e =>
        {
            e.ToTable("RoleClaims", "Membership");
        });
        builder.Entity<MUserClaim>(e =>
        {
            e.ToTable("UserClaims", "Membership");
        });
        builder.Entity<MUserToken>(e =>
        {
            e.ToTable("UserTokens", "Membership");
        });
        builder.Entity<MUserLogin>(e =>
        {
            e.ToTable("UserLogins", "Membership");
        });
        builder.Entity<MUserRole>(e =>
        {
            e.ToTable("UserRoles", "Membership");
        });

    }

}
