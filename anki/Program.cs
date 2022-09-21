// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Net.Sockets;
using Anki;
using ankiapp;

Console.WriteLine("Добро пожаловать в Anki");


var cardService = new CardService();
Card randomCard = new Card();
Console.WriteLine("1-New random card, 2-Fail (1 min) 3-ok (2 min), 4-good (4 min), 5-Create new card");
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
        }

        if (key.KeyChar == '3')
        {
            randomCard.Time = randomCard.Time.AddMinutes(2);
        }
        if (key.KeyChar == '4')
        {
            randomCard.Time =randomCard.Time.AddMinutes(4);
        }

        if (key.KeyChar == '5')
        {
            Console.WriteLine("введите свою карточку:");
            var front = Console.ReadLine();
            var back = Console.ReadLine();
            cardService.AddCard(front,back);
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
            new Card { Front = "mom", Back = "мама", Time = DateTime.Now},
            new Card { Front = "father", Back = "папа", Time = DateTime.Now },
            new Card { Front = "sad", Back = "грустный", Time = DateTime.Now },
            new Card { Front = "good", Back = "хорошо", Time = DateTime.Now },
            new Card { Front = "great", Back = "отлично", Time = DateTime.Now }
        };

        public Card GetRandomOverdueCard()
        {
            var overdueCards = GetOverdueCards();
            var randomNumber = new Random().Next(0, overdueCards.Count);
            
            return overdueCards[randomNumber];
        }

        public List<Card> GetOverdueCards()
        {
            var ovedueCards = new List<Card>();

            foreach (var card in Cards)
            {
                if (card.Time <= DateTime.Now)
                {
                    ovedueCards.Add(card);
                }
            }
            return ovedueCards;
        }

        public void AddCard(string front, string back)
        {
            var card = new Card();

            card.Front = front;
            card.Back = back;
            card.Time = DateTime.Now;

            this.Cards.Add(card);
        }
    }
}
