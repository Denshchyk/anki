// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Anki;
using ankiapp;

Console.WriteLine("Добро пожаловать в Anki");


var cardService = new CardService();
Card randomCard = new Card();
Console.WriteLine("1-New random card, 2-Fail (1 min) 3-ok (2 min), 4-good (4 min), 5-Create new card, 6-Delete this card");
Console.WriteLine("Press ESC to stop");
do {
    while (! Console.KeyAvailable) 
    {

        var key = Console.ReadKey();

        if (key.KeyChar == '1')
        {
            randomCard = cardService.GetRandomOverdueCard();
            Console.WriteLine(randomCard.Front);
        }
         
        if (key.KeyChar == '2')
        {
           randomCard.Time = randomCard.Time.AddMinutes(1);
           Console.WriteLine("you see this word after 1 minute");
           Console.WriteLine(randomCard.Back);
        }

        if (key.KeyChar == '3')
        {
            randomCard.Time = randomCard.Time.AddMinutes(2);
            Console.WriteLine("you see this word after 2 minutes");
            Console.WriteLine(randomCard.Back);
        }
        if (key.KeyChar == '4')
        {
            randomCard.Time = randomCard.Time.AddMinutes(4);
            Console.WriteLine("you see this word after 4 minutes");
            Console.WriteLine(randomCard.Back);
        }

        if (key.KeyChar == '5')
        {
            Console.WriteLine("введите свою карточку: 1 - front, 2 - Back, 3 - ID");
            var front = Console.ReadLine();
            var back = Console.ReadLine();
            cardService.AddCard(front,back);
        }

        if (key.KeyChar == '6')
        {
            Console.WriteLine("\n" + randomCard.Id);
            var readLine = Console.ReadLine();
            var deleteCard = cardService.DeleteCard(readLine);
            Console.WriteLine(deleteCard.Front + " delete");
        }

    }       
} while (Console.ReadKey(true).Key != ConsoleKey.Escape);



//удаление карточки

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

        public Card DeleteCard(string id)
        {
            var cardDelete = Cards.SingleOrDefault(card => card.Id.ToString() == id);
            Cards.Remove(cardDelete);
            return cardDelete;
        }

    }
}
