namespace anki.Domain.Models;

public record CardTagModel(Guid cardId, Guid tagId);
public class CardTag
{
    public CardTag(Guid cardId, Guid tagId)
    {
        CardId = cardId;
        TagId = tagId;
    }
    public Guid CardId { get; set; }
    public virtual Card Card { get; set; }

    public Guid TagId { get; set; }
    public virtual Tag Tag { get; set; }
}
