using anki.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace anki.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CardsController : ControllerBase
{
    private CardService _cardService;

    public CardsController()
    {
        _cardService = new CardService(new CardRepository());
    }
    
    [HttpGet]
    public IEnumerable<Card> Get()
    {
        return _cardService.GetAll();
    }
}