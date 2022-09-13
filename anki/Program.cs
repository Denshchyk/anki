// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Anki;
using ankiapp;

Console.WriteLine("Добро пожаловать в Anki");

Console.WriteLine("1-Fail (1 min) 2-ok (2 min), 3-good (4 min)");


DateTime d1 = DateTime.Now;
var cardService = new CardService();
var randomCard = cardService.GetRandomCard();
Console.WriteLine(randomCard.Front);

var key = Console.ReadKey();

if (key.KeyChar == '1')
{
    d1.AddMinutes(1);
}
if (key.KeyChar == '2')
{
   d1.AddMinutes(2);
}

if (key.KeyChar == '3')
{
    d1.AddMinutes(4);
}
else Console.WriteLine("вы нажали не правильную кнопку, выберите из списка");


//если слово зафейлил, то повторять слово через 1 минуту, если хорошо, то черещ 2, если отлично, то через 4 минуты
//занести слова, потом переделать под базу данных с вводом

namespace ankiapp
{
    public class CardService
    {
        public List<Card> Cards = new List<Card>
        {
            new Card { Front = "mom", Back = "мама" },
            new Card { Front = "father", Back = "папа" },
            new Card { Front = "sad", Back = "грустный" },
            new Card { Front = "good", Back = "хорошо" },
            new Card { Front = "great", Back = "отлично" }
        };

        public Card GetRandomCard()
        {
            var randomNumber = new Random().Next(0, Cards.Count);
            
            return Cards [randomNumber];
        }
    }
}
