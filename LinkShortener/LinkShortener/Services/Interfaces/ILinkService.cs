using LinkShortener.Entities;

namespace LinkShortener.Services.Interfaces;

public interface ILinkService
{
    public Task<Link> GetLinkByIdAsync(Guid id);
    public Task CreateLinkAsync(string fullUrl);
    public Task UpdateLinkAsync(Link newLink);
    public Task DeleteLinkAsync(Guid id);
    public Task<IEnumerable<Link>> GetAllLinksAsync();
}