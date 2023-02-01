namespace anki.Domain;

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
    
    public List<CardTag> CardTags { get; set; }
    
    public override string ToString()
    {
        return $"{Front} - {Back}";
    }
}

