using anki.Domain.Models;

namespace anki.Domain.Repositories;

public interface ICardTagsRepository
{
    Task AddCardTagAsync(CardTag addCardTag);
    void DeleteCard(IEnumerable<Tag> tagsByTagId);
    void DeleteTag(IEnumerable<Card> cardsByCardId);
    Task<CardTag?> DeleteCardTagAsync(CardTag? deleteCardTag);
    IEnumerable<Card> GetAllCardsByTagId(Guid? tagId);
    IEnumerable<Tag> GetAllTagsByCardId(string id);
    Task<CardTag> GetCardTagByTagIdAndCardId(Guid cardId, Guid tagId);
    List<CardTag> GetAllCardTags();
}