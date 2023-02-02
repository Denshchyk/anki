namespace anki.Domain;

public class Tag
{
    public Tag(Guid tagId, string name)
    {
        TagId = tagId;
        Name = name;
    }
    public Guid TagId { get; set; }
    
    public string Name { get; set; }

    public List<CardTag> CardTags { get; set; } = new List<CardTag>();
}
