using anki.Domain.Models;

namespace anki.Domain.Repositories;

public interface ICardTagsRepository
{
    Task AddCardTagAsync(CardTag addCardTag);
    Task<CardTag> DeleteCardTagAsync(IEnumerable<Tag> deleteCardTag);
    IEnumerable<Card> GetAllCardsByTagId(Guid? tagId);
    IEnumerable<Tag> GetAllTagsByCardId(Guid Id);
}