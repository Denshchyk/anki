using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using Anki;
using ankiapp;
using Microsoft.EntityFrameworkCore;

namespace anki;

public class CardRepository
{
    public Card AddCard(Card addCard)
    {
        using ApplicationContext db = new ApplicationContext();
        db.Cards.Add(addCard);
        db.SaveChanges();
        return addCard;
    }

    public Card UpdateCard(Card card)
    {
        using ApplicationContext db = new ApplicationContext();
        if (card != null)
        {
            db.Cards.Update(card);
            db.SaveChanges();
        }
        return card;
    }

    public Card RemoveCard(Card card)
    {
        using ApplicationContext db = new ApplicationContext();
        db.Cards.Remove(card);
        db.SaveChanges();
        return card;
    }

    public Card? GetById(Guid id)
    {
        using ApplicationContext db = new ApplicationContext();
        var card = db.Cards.AsNoTracking().FirstOrDefault(card => card.Id == id);
        return card;
        
    }
    public Card? GetById(string id)
    {
        var guid = Guid.Parse(id);
        using ApplicationContext db = new ApplicationContext();
        var card = db.Cards.AsNoTracking().FirstOrDefault(card => card.Id == guid);
        return card;
    }
    public List<Card> GetAll()
    {
        using ApplicationContext db = new ApplicationContext();
        return db.Cards.ToList();
    }
}