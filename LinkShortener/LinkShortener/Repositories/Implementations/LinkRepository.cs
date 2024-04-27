using LinkShortener.Entities;
using LinkShortener.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Repositories.Implementations;

public class LinkRepository : ILinkRepository
{
    private readonly AppDbContext _db;

    public LinkRepository(AppDbContext db) => _db = db;

    public async Task CreateAsync(Link link)
    {
        await _db.Links.AddAsync(link);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Link link)
    {
        _db.Links.Update(link);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Link link)
    {
        _db.Links.Remove(link);
        await _db.SaveChangesAsync();
    }

    public async Task<Link> GetByIdAsync(Guid id)
    {
        return _db.Links.FirstOrDefault(link => link.Id.Equals(id));
    }

    public async Task<IEnumerable<Link>> GetAllAsync()
    {
        return await _db.Links.ToListAsync();
    }
}