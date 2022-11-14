using System.Text.Json;
using anki;
using Anki;

namespace ankiapp;

public class CardService
{
    private CardRepository _cardRepository;

    public CardService(CardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public Card GetRandomOverdueCard()
    {
        var overdueCards = GetOverdueCards();
        var randomNumber = new Random().Next(0, overdueCards.Count);
            
        return overdueCards[randomNumber];
    }
    
    public List<Card> GetOverdueCards()
    {
        return _cardRepository.GetAll().Where(card => card.Time <= DateTime.UtcNow).ToList();
    }

    public async Task AddCard(string front, string back)
    {
        var card = new Card(front,back);
            
        card.Id = Guid.NewGuid();
        card.Time = DateTime.UtcNow;
        
       await _cardRepository.AddCardAsync(card);
    }

    public async Task<Card> DeleteCardIdAsync(string id)
    {
        var cardDeleteId = await _cardRepository.GetByIdAsync(id);
        ThrowExceptionIfCardNull(cardDeleteId);
        await _cardRepository.RemoveCardAsync(cardDeleteId);
        return cardDeleteId;
    }

    private static void ThrowExceptionIfCardNull(Card? cardDeleteId)
    {
        if (cardDeleteId == null)
        {
            throw new NotFoundException("Card is not found");
        }
    }

    public async Task<Card> UpdateCardAsync (Guid id)
    {
        var cardUpdate = await _cardRepository.GetByIdAsync(id);
        ThrowExceptionIfCardNull(cardUpdate);
        await _cardRepository.UpdateCardAsync(cardUpdate);
        return cardUpdate;
    }

    public Task<Card> DeleteCard(Card card)
    {
        var cardDelete = _cardRepository.RemoveCardAsync(card);
        return cardDelete;
    }

    public Card SaveCardToJson(string front, string back)
    {
        using FileStream fs = new FileStream("Cards.json", FileMode.OpenOrCreate);
        var newCard = new Card(front, back); 
        JsonSerializer.Serialize<Card>(fs, newCard);
        Console.WriteLine("Card has been saved to file");
        return newCard;
    }

    public Card ReadCardFromJson(string front, string back)
    {
        using FileStream fs = new FileStream("Cards.json", FileMode.OpenOrCreate);
        Card? card = JsonSerializer.Deserialize<Card>(fs);
        Console.WriteLine($"Front: {card?.Front}  Back: {card?.Back}");
        return card;
    }
}