using System.Text;
using LinkShortener.Entities;
using LinkShortener.Repositories.Interfaces;
using LinkShortener.Services.Interfaces;
using LinkShortener.Tools.Interfaces;

namespace LinkShortener.Services.Implementations;

public class LinkService : ILinkService
{
    private readonly ILinkRepository _linkRepository;
    private readonly IShortener _shortener;
    
    private string _baseUrl = "https://localhost:7018";

    public LinkService(ILinkRepository linkRepository, IShortener shortener)
    {
        _linkRepository = linkRepository;
        _shortener = shortener;
    }

    public async Task<Link> GetLinkByIdAsync(Guid id)
    {
        var link = await _linkRepository.GetByIdAsync(id);
        if (link is null)
        {
            throw new Exception($"Link with id [{id}] not found.");
        }

        return link;
    }

    public async Task CreateLinkAsync(string fullUrl)
    {
        string generatedUrl = _shortener.GenerateShortUrl(fullUrl);
        
        Link newLink = new Link()
        {
            FullUrl = fullUrl,
            ShortUrl = $"{_baseUrl}/{generatedUrl}",
            CountClicks = 0,
            CreationDate = DateOnly.FromDateTime(DateTime.Today)
        };

        await _linkRepository.CreateAsync(newLink);
    }

    public async Task UpdateLinkAsync(Link link)
    {
        var forCheck = await _linkRepository.GetByIdAsync(link.Id);
        if (forCheck is null)
        {
            throw new Exception($"Link with id [{link.Id}] not found. Update error.");
        }
        
        string generatedUrl = _shortener.GenerateShortUrl(link.FullUrl);
        link.ShortUrl = $"{_baseUrl}/{generatedUrl}";

        await _linkRepository.UpdateAsync(link);
    }

    public async Task DeleteLinkAsync(Guid id)
    {
        var link = await _linkRepository.GetByIdAsync(id);
        if (link is null)
        {
            throw new Exception($"Link with id [{id}] not found. Delete error.");
        }

        await _linkRepository.DeleteAsync(link);
    }

    public async Task<IEnumerable<Link>> GetAllLinksAsync()
    {
        var links = await _linkRepository.GetAllAsync();

        return links;
    }
}