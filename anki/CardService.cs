using System.Runtime.ExceptionServices;
using Anki;

namespace ankiapp
{
    public class CardService
    {
        public List<Card> Cards = new List<Card>
        {
            new Card { Front = "mom", Back = "мама", Time = DateTime.Now, Id = Guid.NewGuid()},
            new Card { Front = "father", Back = "папа", Time = DateTime.Now, Id = Guid.NewGuid()},
            new Card { Front = "sad", Back = "грустный", Time = DateTime.Now, Id = Guid.NewGuid()},
            new Card { Front = "good", Back = "хорошо", Time = DateTime.Now, Id = Guid.NewGuid()},
            new Card { Front = "great", Back = "отлично", Time = DateTime.Now, Id = Guid.NewGuid()}
        };

        public Card GetRandomOverdueCard()
        {
            var overdueCards = GetOverdueCards();
            var randomNumber = new Random().Next(0, overdueCards.Count);
            
            return overdueCards[randomNumber];
        }

        public List<Card> GetOverdueCards()
        {
            return Cards.Where(card => card.Time <= DateTime.Now).ToList();
        }

        public void AddCard(string front, string back)
        {
            var card = new Card();

            card.Front = front;
            card.Back = back;
            card.Id = Guid.NewGuid();
            card.Time = DateTime.Now;

            this.Cards.Add(card);
        }

        public Card DeleteCardId(string id)
        {
            var cardDeleteId = Cards.SingleOrDefault(card => card.Id.ToString() == id);
            Cards.Remove(cardDeleteId);
            return cardDeleteId;
        }

        public Card UpdateCard (string id, string front, string back)
        {
            var cardUpdate = Cards.SingleOrDefault(card => card.Id.ToString() == id);
            cardUpdate.Front = front;
            cardUpdate.Back = back;
            return cardUpdate;
        }

        public Card DeleteCard(string front, string back)
        {
            var cardDelete = Cards.SingleOrDefault(card => card.Front.ToString() == front && card.Back.ToString() == back);
            Cards.Remove(cardDelete);
            return cardDelete;
        }
    }
}