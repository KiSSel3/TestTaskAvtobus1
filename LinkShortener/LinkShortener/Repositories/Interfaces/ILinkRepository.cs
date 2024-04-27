using LinkShortener.Entities;

namespace LinkShortener.Repositories.Interfaces;

public interface ILinkRepository
{
    public Task CreateAsync(Link link);
    public Task UpdateAsync(Link link);
    public Task DeleteAsync(Link link);

    public Task<Link> GetByIdAsync(Guid id);
    
    public Task<IEnumerable<Link>> GetAllAsync();
}