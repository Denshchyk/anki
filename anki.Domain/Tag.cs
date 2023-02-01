namespace anki.Domain;

public class Tag
{
    public Tag(Guid tagId, string name)
    {
        tagId = Guid.NewGuid();
        name = Name;
    }
    public Guid TagId { get; set; }
    
    public string Name { get; set; }
    
    public List<CardTag> CardTags { get; set; }
}
