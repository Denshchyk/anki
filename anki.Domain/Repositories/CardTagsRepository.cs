using anki.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace anki.Domain.Repositories;

public class CardTagsRepository : ICardTagsRepository
{
    private readonly ApplicationContext _context;

    public CardTagsRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddCardTagAsync(CardTag? addCardTag)
    {
        if (addCardTag != null) await _context.CardTags.AddAsync(addCardTag);
        await _context.SaveChangesAsync();
    }
    
    public void DeleteCard(IEnumerable<Tag> tagsByTagId)
    {
        _context.Tags.RemoveRange(tagsByTagId);
    }
    
    public void DeleteTag(IEnumerable<Card> cardsByCardId)
    {
        _context.Cards.RemoveRange(cardsByCardId);
    }
    
    public async Task<CardTag?> DeleteCardTagAsync(CardTag? deleteCardTag)
    {
       _context.CardTags.Remove(deleteCardTag);
       await _context.SaveChangesAsync();
       return deleteCardTag;
    }
    
    public IEnumerable<Card> GetAllCardsByTagId(Guid? tagId)
    {
        return _context.CardTags.Where(x => x.TagId.Equals(tagId)).Select(x=> x.Card);
    }

    public IEnumerable<Tag> GetAllTagsByCardId(Guid id)
    {
        return _context.CardTags.Where(x => x.CardId.Equals(id)).Select(x => x.Tag);
    }

    public async Task<CardTag> GetCardTagByTagIdAndCardId(Guid cardId, Guid tagId)
    {
        return await _context.CardTags.FirstOrDefaultAsync(x => x.CardId.Equals(cardId) && x.TagId.Equals(tagId));
    }
}