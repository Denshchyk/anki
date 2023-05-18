namespace anki.Domain.Models;

public record TagModel(string Name, IEnumerable<string> Cards);
public class Tag
{
    public Tag(Guid tagId, string name)
    {
        TagId = tagId;
        Name = name;
    }
    public Guid TagId { get; set; }
    
    public string Name { get; set; }

    public virtual List<CardTag>? CardTags { get; set; } = new List<CardTag>();
}
