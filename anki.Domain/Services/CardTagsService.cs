using System.Globalization;
using System.Xml.Schema;
using anki.Domain.Models;
using anki.Domain.Repositories;

namespace anki.Domain.Services;

public class CardTagsService
{
    private CardTagsRepository _cardTagsRepository;

    public CardTagsService(CardTagsRepository cardTagRepository)
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
        var cardTag = new CardTag(cardId, tagId);
        var getCardTag = _cardTagsRepository.GetCardTagByTagIdAndCardId(cardTag);
        ThrowExceptionIfCardTagNull(getCardTag);
        await _cardTagsRepository.DeleteCardTagAsync(getCardTag);
    }

    private void ThrowExceptionIfCardTagNull(object getCardTag)
    {
        throw new NotImplementedException();
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

    public async Task<IEnumerable<Tag>> DeleteAllCardTagFromOneCardAsync(Guid cardId)
    {
        var getCardTagsByCardId = _cardTagsRepository.GetAllTagsByCardId(cardId);
        ThrowExceptionIfCardNull(cardId);
        var cardTagsByCardId = getCardTagsByCardId.ToList();
        await _cardTagsRepository.DeleteCardTagAsync(cardTagsByCardId);
        return cardTagsByCardId;
    }
    private static void ThrowExceptionIfCardNull(Guid? cardId)
    {
        if (cardId == null)
        {
            throw new NotFoundException("Card is not found");
        }
    }
    
    public async Task<IEnumerable<Card>> DeleteAllCardTagFromOneTagAsync(Guid? tagId)
    {
        var getCardTagsByTagId = _cardTagsRepository.GetAllCardsByTagId(tagId);
        ThrowExceptionIfTagNull(tagId);
        var cardTagsByTagId = getCardTagsByTagId.ToList();
        await _cardTagsRepository.DeleteCardTagAsync(cardTagsByTagId);
        return cardTagsByTagId;
    }
    private static void ThrowExceptionIfTagNull(Guid? tagId)
    {
        if (tagId == null)
        {
            throw new NotFoundException("Tag is not found");
        }
    }
}