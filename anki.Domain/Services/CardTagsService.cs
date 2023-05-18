using System.Globalization;
using System.Xml.Schema;
using anki.Domain.Models;
using anki.Domain.Repositories;

namespace anki.Domain.Services;

public class CardTagsService : ICardTagsService
{
    private ICardTagsRepository _cardTagsRepository;

    public CardTagsService(ICardTagsRepository cardTagRepository)
    {
        _cardTagsRepository = cardTagRepository;
    }

    public async Task AddCardTagsAsync(Guid cardId, Guid tagId)
    {
        var addCardTag = new CardTag(cardId, tagId);
        await _cardTagsRepository.AddCardTagAsync(addCardTag);
    }

    public async Task DeleteOneCardTag(Guid cardId, Guid tagId)
    {
        var getCardTag = await GetCardTagByTagIdAndCardId(cardId, tagId);
        ThrowExceptionIfCardTagNull(cardId, tagId);
        await _cardTagsRepository.DeleteCardTagAsync(getCardTag);
    }
    
    public async Task<CardTag> GetCardTagByTagIdAndCardId(Guid cardId, Guid tagId)
    {
        return await _cardTagsRepository.GetCardTagByTagIdAndCardId(cardId,tagId);
    }

    public IEnumerable<Tag> GetAllTagsByCardId(string cardId)
    {
        return _cardTagsRepository.GetAllTagsByCardId(cardId);
    }

    public IEnumerable<Card> GetAllCardsByTagId(Guid tagId)
    {
        return _cardTagsRepository.GetAllCardsByTagId(tagId);
    }

    private static void ThrowExceptionIfCardTagNull(Guid cardId, Guid tagId)
    {
        if (cardId == null)
        {
            throw new NotFoundException("Card is not found");
        }

        if (tagId == null)
        {
            throw new NotFoundException("Tag is not found");
        }
    }

    public IEnumerable<Tag> DeleteAllCardTagFromOneCard(string cardId)
    {
        ThrowExceptionIfCardNull(cardId);
        var getTagsByCardId = _cardTagsRepository.GetAllTagsByCardId(cardId);
        _cardTagsRepository.DeleteCard(getTagsByCardId);
        return getTagsByCardId;
    }
    private static void ThrowExceptionIfCardNull(string cardId)
    {
        if (cardId == null)
        {
            throw new NotFoundException("Card is not found");
        }
    }
    
    public IEnumerable<Card> DeleteAllCardTagFromOneTag(Guid tagId)
    {
        ThrowExceptionIfTagNull(tagId);
        var cards = GetAllCardsByTagId(tagId);
        _cardTagsRepository.DeleteTag(cards);
        return cards;
    }
    
    private static void ThrowExceptionIfTagNull(Guid? tagId)
    {
        if (tagId == null)
        {
            throw new NotFoundException("Tag is not found");
        }
    }
}