// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using anki;
using Anki;
using ankiapp;
using Microsoft.VisualBasic.FileIO;

Console.WriteLine("Добро пожаловать в Anki");


var cardService = new CardService();
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
            Console.WriteLine("введите свою карточку: 1 - front, 2 - Back");
            var front = Console.ReadLine();
            var back = Console.ReadLine();
            cardService.AddCard(front,back);
        }

        if (key.KeyChar == '6')
        {
            Console.WriteLine("\n" + randomCard.Id);
            try
            {
                var readLine = Console.ReadLine();
                var deleteCard = cardService.DeleteCardId(readLine);
                Console.WriteLine(deleteCard.Front + " delete");
            }
            catch (Exception notFoundException)
            {
                throw new NotFoundException("Card is not found");
            }
        }

        if (key.KeyChar == '7')
        {
            Console.WriteLine("\n" + randomCard.Id);
            var cardUpdate = cardService.UpdateCard(Console.ReadLine(), Console.ReadLine(), Console.ReadLine());
            Console.WriteLine(cardUpdate.Front + cardUpdate.Back +"Card updated");
        }
        if (key.KeyChar == '8')
        {
            Console.WriteLine("введите карточку для удаления: 1 - front, 2 - Back");
            var front = Console.ReadLine();
            var back = Console.ReadLine();
            var deleteCard = cardService.DeleteCard(front, back);
            Console.WriteLine(deleteCard.Front + " " + deleteCard.Back + " delete");
        }
    }       
} while (Console.ReadKey(true).Key != ConsoleKey.Escape);