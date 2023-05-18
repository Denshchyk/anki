using System.Text.Json;
using anki.Domain.Interfases;
using anki.Domain.Models;
using anki.Domain.Repositories;

namespace anki.Domain.Services;

public class CardService : ICardService
{
    private ICardRepository _cardRepository;
    private ICardTagsRepository _cardTagsRepository;

    public CardService(ICardRepository cardRepository, ICardTagsRepository cardTagsRepository)
    {
        _cardRepository = cardRepository;
        _cardTagsRepository = cardTagsRepository;
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
    
    public async Task AddCardWithTagAsync(string front, string back, string nameTag)
    {
        var card = new Card(front, back);

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
        await _cardRepository.UpdateCardAsync(card);
    }

    public async Task<Card> UpdateCardAsync (string id, CardModel card)
    {
        var getCard = await _cardRepository.GetByIdAsync(id);
        getCard.Back = card.Back;
        getCard.Front = card.Front;
        await _cardRepository.UpdateCardAsync(getCard);
        return getCard;
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

    public List<CardModel> GetAll()
    { 
        var cards = _cardRepository.GetAll();
        var tags = _cardTagsRepository.GetAllCardTags().Select(x => x.Tag).ToList();
        return cards.Select(x => new CardModel(x.Id.ToString(), x.Front, x.Back, x.CardTags.Select(t => t.Tag.Name))).ToList();
    }

    public async Task<CardModel> GetByIdAsync(string id)
    {
        var card = await _cardRepository.GetByIdAsync(id);
        var tags = _cardTagsRepository.GetAllTagsByCardId(id).Select(x=> x.Name).ToList();
        return new CardModel(card.Id.ToString(), card.Front, card.Back, tags);
    }

    public async Task<Card?> GetByFrontAndBackAsync(string front, string back)
    {
        return await _cardRepository.GetByFrontAndBackAsync(front, back);
    }
    public async Task AddCardAsync(Card card)
    {
        card.Id = Guid.NewGuid();
        card.Time = DateTime.UtcNow;
        
        await _cardRepository.AddCardAsync(card);
    }
}