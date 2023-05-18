using anki.Domain.Models;

namespace anki.Domain.Services;

public interface ICardTagsService
{
    Task AddCardTagsAsync(Guid cardId, Guid tagId);
    Task DeleteOneCardTag(Guid cardId, Guid tagId);
    Task<CardTag> GetCardTagByTagIdAndCardId(Guid cardId, Guid tagId);
    IEnumerable<Tag> GetAllTagsByCardId(string cardId);
    IEnumerable<Card> GetAllCardsByTagId(Guid tagId);
    IEnumerable<Tag> DeleteAllCardTagFromOneCard(string cardId);
    IEnumerable<Card> DeleteAllCardTagFromOneTag(Guid tagId);
}