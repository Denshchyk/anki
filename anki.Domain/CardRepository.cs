using Microsoft.EntityFrameworkCore;

namespace anki.Domain;

public class CardRepository : ICardRepository
{
    public async Task AddCardAsync(Card addCard)
    {
        await using ApplicationContext db = new ApplicationContext();
        await db.Cards.AddAsync(addCard);
        await db.SaveChangesAsync();
    }

    public async Task UpdateCardAsync(Card card)
    {
        await using ApplicationContext db = new ApplicationContext();
        db.Cards.Update(card);
        await db.SaveChangesAsync();
    }

    public async Task<Card> RemoveCardAsync(Card card)
    {
        await using ApplicationContext db = new ApplicationContext();
        db.Cards.Remove(card);
        await db.SaveChangesAsync();
        return card;
    }

    public async Task<Card?> GetByIdAsync(Guid id)
    {
        await using ApplicationContext db = new ApplicationContext();
        var card = await db.Cards.AsNoTracking().FirstOrDefaultAsync(card => card.Id == id);
        return card;
    }
    public async Task<Card?> GetByIdAsync(string id)
    {
        var guid = Guid.Parse(id);
        await using ApplicationContext db = new ApplicationContext();
        var card = await db.Cards.AsNoTracking().FirstOrDefaultAsync(card => card.Id == guid);
        return card;
    }
    public async Task<Card?> GetByFrontAndBackAsync(string front, string back)
    {
        await using ApplicationContext db = new ApplicationContext();
        var card = await db.Cards.AsNoTracking().FirstOrDefaultAsync(card => card.Front == front && card.Back == back);
        return card;
    }
    public List<Card> GetAll()
    {
        using ApplicationContext db = new ApplicationContext();
        return db.Cards.ToList();
    }
}