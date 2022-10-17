using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using ankiapp;

namespace Anki;

public class Card
{
    public Card(string front, string back)
    {
        Front = front;
        Back = back;
        Time = DateTime.Now;
        Id = Guid.NewGuid();
    }
    public string Front { get; set; }
    
    public string Back { get; set; }

    public DateTime Time { get; set; }
    
    public Guid Id { get; set; }
    
    
    public override string ToString()
    {
        return $"{Front} - {Back}";
    }
}

