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
        return _cardRepository.GetAll().Where(card => card.Time <= DateTime.Now).ToList();
    }

    public void AddCard(string front, string back)
    {
        var card = new Card(front,back);
            
        card.Id = Guid.NewGuid();
        card.Time = DateTime.UtcNow;
        
       _cardRepository.AddCard(card);
    }

    public Card DeleteCardId(string id)
    {
        var cardDeleteId = _cardRepository.GetById(id);
        ThrowExceptionIfCardNull(cardDeleteId);
        _cardRepository.RemoveCard(cardDeleteId);
        return cardDeleteId;
    }

    private static void ThrowExceptionIfCardNull(Card? cardDeleteId)
    {
        if (cardDeleteId == null)
        {
            throw new NotFoundException("Card is not found");
        }
    }

    public Card UpdateCard (Guid id)
    {
        var cardUpdate = _cardRepository.GetById(id);
        ThrowExceptionIfCardNull(cardUpdate);
        _cardRepository.UpdateCard(cardUpdate);
        return cardUpdate;
    }

    public Card DeleteCard(Card card)
    {
        var cardDelete = _cardRepository.RemoveCard(card);
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