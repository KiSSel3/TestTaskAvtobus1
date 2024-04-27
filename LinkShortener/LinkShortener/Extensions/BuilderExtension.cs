using LinkShortener.Repositories;
using LinkShortener.Repositories.Implementations;
using LinkShortener.Repositories.Interfaces;
using LinkShortener.Services.Implementations;
using LinkShortener.Services.Interfaces;
using LinkShortener.Tools.Implementations;
using LinkShortener.Tools.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Extensions;

public static class BuilderExtension
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        //MVC
        builder.Services.AddControllersWithViews();

        //Repository
        builder.Services.AddScoped<ILinkRepository, LinkRepository>();

        //Service
        builder.Services.AddScoped<ILinkService, LinkService>();
        
        //Other
        builder.Services.AddScoped<IShortener, Shortener>();
    }
    
    public static void AddDataBase(this WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(11, 3, 2))));
        builder.Services.AddDbContext<AppDbContext>();
    }
}