namespace LinkShortener.Tools.Interfaces;

public interface IShortener
{
    public string GenerateShortUrl(string fullUrl);
}