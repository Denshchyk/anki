namespace anki.Domain.Models;

public class CardTag
{
    public Guid CardId { get; set; }
    public Card Card { get; set; }

    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}
