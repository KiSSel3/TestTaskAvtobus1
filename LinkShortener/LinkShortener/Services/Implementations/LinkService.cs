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
        bool isUnique = await UniqueCheck(fullUrl);
        if (!isUnique)
        {
            throw new Exception("This url already exists.");
        }
        
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

    public async Task UpdateLinkAsync(Link newLink)
    {
        var link = await _linkRepository.GetByIdAsync(newLink.Id);
        if (link is null)
        {
            throw new Exception($"Link with id [{newLink.Id}] not found. Update error.");
        }

        if (!link.FullUrl.Equals(newLink.FullUrl))
        {
            bool isUnique = await UniqueCheck(newLink.FullUrl);
            if (!isUnique)
            {
                throw new Exception("This url already exists.");
            }

            link.FullUrl = newLink.FullUrl;
            
            string generatedUrl = _shortener.GenerateShortUrl(newLink.FullUrl);
            link.ShortUrl = $"{_baseUrl}/{generatedUrl}";
        }

        link.CountClicks = newLink.CountClicks;

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

    private async Task<bool> UniqueCheck(string fullUrl)
    {
        var allLinks = await _linkRepository.GetAllAsync();

        if (allLinks.Count(link => link.FullUrl.Equals(fullUrl)) != 0)
        {
            return false;
        }

        return true;
    }
}