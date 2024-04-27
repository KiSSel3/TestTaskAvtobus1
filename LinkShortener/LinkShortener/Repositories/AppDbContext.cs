using LinkShortener.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<Link> Links { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}