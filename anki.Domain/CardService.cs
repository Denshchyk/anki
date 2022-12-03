using System.Runtime.InteropServices;
using System.Text.Json;

namespace anki.Domain;

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

    public async Task AddCardAsync(string front, string back)
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

    public async Task AddMinutesToCard(Card card, int minutes)
    {
        card.Time = DateTime.UtcNow;
        card.Time = card.Time.AddMinutes(minutes);
        await UpdateCardAsync(card);
    }

    public async Task<Card> UpdateCardAsync (Card card)
    {
        var cardUpdate = await _cardRepository.GetByIdAsync(card.Id);
        ThrowExceptionIfCardNull(cardUpdate);
        await _cardRepository.UpdateCardAsync(card);
        return card;
    }

    public async Task<Card> DeleteCardAsync(Card card)
    {
        var cardDelete = await _cardRepository.RemoveCardAsync(card);
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

    public List<Card> GetAll()
    {
       return _cardRepository.GetAll();
    }

    public async Task<Card?> GetByIdAsync(string id)
    {
        return await _cardRepository.GetByIdAsync(id);
    }

    public async Task<Card?> GetByFrontAndBackAsync(string front, string back)
    {
        return await _cardRepository.GetByFrontAndBackAsync(front, back);
    }
}