using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkShortener.Entities;

public class Link
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public string FullUrl { get; set; }
    public string ShortUrl { get; set; }
    
    public int CountClicks { get; set; }
    
    public DateOnly CreationDate { get; set; }
}