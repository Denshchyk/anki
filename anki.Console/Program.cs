// See https://aka.ms/new-console-template for more information

using System.Data.Common;
using System.Runtime.InteropServices;
using anki;
using anki.Domain;

Console.WriteLine("Добро пожаловать в Anki");

var cardService = new CardService(new CardRepository());
Card randomCard = new Card("Front", "Back");


Console.WriteLine("1-New random card, 2-Fail (1 min) 3-ok (2 min), 4-good (4 min), 5-Create new card, 6-Delete this card, 7-Update card, 8 -Delete card with front and back");
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
           await cardService.AddMinutesToCard(randomCard, 1);
           Console.WriteLine("you see this word after 1 minute");
           Console.WriteLine(randomCard.Back);
        }

        if (key.KeyChar == '3')
        {
            await cardService.AddMinutesToCard(randomCard, 2);
            Console.WriteLine("you see this word after 2 minutes");
            Console.WriteLine(randomCard.Back);
        }
        if (key.KeyChar == '4')
        {
            await cardService.AddMinutesToCard(randomCard, 4);
            Console.WriteLine("you see this word after 4 minutes");
            Console.WriteLine(randomCard.Back);
        }

        if (key.KeyChar == '5')
        {
            Console.WriteLine("введите свою карточку: 1 - front, 2 - Back");
            var front = Console.ReadLine();
            var back = Console.ReadLine();
            cardService.AddCard(front,back);
            cardService.SaveCardToJson(front, back);
        }

        if (key.KeyChar == '6')
        {
            Console.WriteLine("\n" + randomCard.Id);
            try
            {
                var readLine = Console.ReadLine();
                var deleteCard = cardService.DeleteCardIdAsync(readLine);
                Console.WriteLine(deleteCard + " delete");
            }
            catch (Exception notFoundException)
            {
                Console.WriteLine(notFoundException.Message);
            }
        }

        if (key.KeyChar == '7')
        {
            try
            {
                Console.WriteLine("\n" + randomCard.Id);
                Console.WriteLine("Введите фронт");
                var front = Console.ReadLine();
                Console.WriteLine("Введите бэк");
                var back = Console.ReadLine();
                randomCard.Front = front;
                randomCard.Back = back;
                var cardUpdate = cardService.UpdateCardAsync(randomCard);
                Console.WriteLine(cardUpdate +"Card updated");
            }
            catch (Exception notFoundException)
            {
                Console.WriteLine(notFoundException.Message);
            }
        }
        if (key.KeyChar == '8')
        {
            Console.WriteLine("введите карточку для удаления: 1 - front, 2 - Back");
            var front = Console.ReadLine();
            var back = Console.ReadLine();
            var deleteCard = cardService.DeleteCard;
            Console.WriteLine(deleteCard + " " + " delete");
        }
    }       
} while (Console.ReadKey(true).Key != ConsoleKey.Escape);