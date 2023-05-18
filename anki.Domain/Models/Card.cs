namespace anki.Domain.Models;

public record CardModel(string Id, string Front, string Back, IEnumerable<string> Tags);
public class Card
{
    public Card(string front, string back)
    {
        Front = front;
        Back = back;
        Time = DateTime.UtcNow;
        Id = Guid.NewGuid();
    }

    public string Front { get; set; }
    
    public string Back { get; set; }

    public DateTime Time { get; set; }
    
    public Guid Id { get; set; }
    
    public virtual List<CardTag>? CardTags { get; set; }
    
    public override string ToString()
    {
        return $"{Front} - {Back}";
    }
}

