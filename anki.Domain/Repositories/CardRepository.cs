using anki.Domain.Interfases;
using anki.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace anki.Domain.Repositories;

public class CardRepository : ICardRepository
{
    private ApplicationContext _context;

    public CardRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddCardAsync(Card addCard)
    {
        await _context.Cards.AddAsync(addCard);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCardAsync(Card card)
    {
        _context.Cards.Update(card);
        await _context.SaveChangesAsync();
    }

    public async Task<Card> RemoveCardAsync(Card card)
    {
        _context.Cards.Remove(card);
        await _context.SaveChangesAsync();
        return card;
    }

    public async Task<Card?> GetByIdAsync(Guid id)
    {
        var card = await _context.Cards.Include(x=> x.CardTags).AsNoTracking().FirstOrDefaultAsync(card => card.Id == id);
        return card;
    }
    public async Task<Card?> GetByIdAsync(string id)
    {
        var guid = Guid.Parse(id);
        return await GetByIdAsync(guid);
    }
    public async Task<Card?> GetByFrontAndBackAsync(string front, string back)
    {
        var card = await _context.Cards.AsNoTracking().FirstOrDefaultAsync(card => card.Front == front && card.Back == back);
        return card;
    }
    public List<Card> GetAll()
    {
        return _context.Cards.ToList();
    }
}