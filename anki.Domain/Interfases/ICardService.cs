using anki.Domain.Models;

namespace anki.Domain.Interfases;

public interface ICardService
{
    Card GetRandomOverdueCard();
    List<Card> GetOverdueCards();
    Task AddCardAsync(string front, string back);
    Task AddCardAsync(Card card);
    Task<Card> DeleteCardIdAsync(string id);
    Task AddMinutesToCard(Card card, int minutes);
    Task<Card> UpdateCardAsync (Card card);
    Task<Card> DeleteCardAsync(Card card);
    Card SaveCardToJson(string front, string back);
    Card ReadCardFromJson(string front, string back);
    List<Card> GetAll();
    Task<Card?> GetByIdAsync(string id);
    Task<Card?> GetByFrontAndBackAsync(string front, string back);
}