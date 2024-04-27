using System.Text;
using LinkShortener.Tools.Interfaces;

namespace LinkShortener.Tools.Implementations;

public class Shortener : IShortener
{
    public string GenerateShortUrl(string fullUrl)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(fullUrl);
        
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(bytes);
            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            
            return sb.ToString().Substring(0, 8);
        }
    }
}