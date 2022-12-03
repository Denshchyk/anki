using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<Card>> GetCard(string id)
    {
        var card = await _cardService.GetByIdAsync(id);

        if (card == null)
        {
            return NotFound();
        }
        return card;
    }

    [HttpDelete("{id}")]

    public async Task<ActionResult<Card>> DeleteCardId(string id)
    {
        var card = await _cardService.DeleteCardIdAsync(id);
        
        if (card == null)
        {
            return NotFound();
        }
        return card;
    }
    [HttpDelete("{front},{back}")]
    public async Task<ActionResult<Card>> DeleteCard(string front, string back, Card card)
    {
        var getCard = await _cardService.GetByFrontAndBackAsync(front, back);
        if (getCard.Front != front && getCard.Back != back)
        {
            return BadRequest();
        }

        if (getCard == null)
        {
            return NotFound();
        }

        return await _cardService.DeleteCardAsync(getCard);
    }

    //`put method = update card
    // get overduecards
    // delete
    // post = 
}