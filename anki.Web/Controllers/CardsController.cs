using System.Runtime.CompilerServices;
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
    private ICardService _cardService;
    
    

    public CardsController(ICardService cardService)
    {
        _cardService = cardService;
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
    public async Task<ActionResult<Card>> DeleteCard(string front, string back)
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

    [HttpPut]
    public async Task<ActionResult<Card>> UpdateCard(Card card)
    {
        if (card == null)
        {
            return NotFound();
        }

        return await _cardService.UpdateCardAsync(card);
    }

    [HttpPost]
    public async Task AddCard(Card card)
    {
        await _cardService.AddCardAsync(card);
    }
}