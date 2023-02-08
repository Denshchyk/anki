using anki.Domain.Models;

namespace anki.Domain.Repositories;

public interface ICardTagsRepository
{
    Task AddCardTagAsync(CardTag addCardTag);
    Task<CardTag> DeleteCardTagAsync(CardTag deleteCardTag);
    Task UpdateCardTagAsync(CardTag updateCardTag);
    IEnumerable<Card> GetAllCardsByTagId(Guid tagId);
    IEnumerable<Tag> GetAllTagsByCardId(Card Id);
}