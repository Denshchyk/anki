namespace anki.Domain.Models;

public class CardTag
{
    public CardTag(Guid cardId, Guid tagId)
    {
        CardId = cardId;
        TagId = tagId;
    }
    public Guid CardId { get; set; }
    public Card Card { get; set; }

    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}
