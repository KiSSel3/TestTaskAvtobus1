using LinkShortener.Repositories;
using LinkShortener.Repositories.Implementations;
using LinkShortener.Repositories.Interfaces;
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

    }
    
    public static void AddDataBase(this WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(11, 3, 2))));
        builder.Services.AddDbContext<AppDbContext>();
    }
}