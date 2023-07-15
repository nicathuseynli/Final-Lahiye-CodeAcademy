using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Final_Lahiye.Services.Interfaces;
using Final_Lahiye.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Services.Implementations;
public class BlogService : IBlogService
{
    private readonly AppDbContext _context;

    public BlogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task Get()
    {

    }
}
