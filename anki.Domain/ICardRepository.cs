namespace anki.Domain;

public interface ICardRepository
{
    Task AddCardAsync(Card addCard);
    Task UpdateCardAsync(Card card);
    Task<Card> RemoveCardAsync(Card card);
    Task<Card?> GetByIdAsync(Guid id);
    Task<Card?> GetByIdAsync(string id);
    Task<Card?> GetByFrontAndBackAsync(string front, string back);
    List<Card> GetAll();
}