using anki.Domain.Models;
using anki.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace anki.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CardTagsController : ControllerBase
{
    private readonly ICardTagsService _cardTagsService;

    public CardTagsController(ICardTagsService cardTagsService)
    {
        _cardTagsService = cardTagsService;
    }
    [HttpPost("{cardId},{tagId}")]
    public async Task AddCardTagAsync(Guid cardId, Guid tagId)
    {
        await _cardTagsService.AddCardTagsAsync(cardId, tagId);
    }
    
    [HttpDelete("{cardId},{tagId}")]
    public async Task<ActionResult<CardTag>> DeleteCardTag(Guid cardId, Guid tagId)
    {
        var cardTag =  _cardTagsService.GetCardTagByTagIdAndCardId(cardId, tagId);

        if (cardTag == null)
        {
            return NotFound();
        }
        
        await _cardTagsService.DeleteOneCardTag(cardId, tagId);
        return NoContent();
    }

    [HttpDelete("{cardId}")]
    public ActionResult<Tag> DeleteAllCardTagsFromOneCard(Guid cardId)
    {
        var tags =  _cardTagsService.GetAllTagsByCardId(cardId);

        if (tags == null)
        {
            return NotFound();
        }

        _cardTagsService.DeleteAllCardTagFromOneCard(cardId);
        return NoContent();
    }
    
    [HttpDelete("{tagId}")]
    public ActionResult<Card> DeleteAllCardTagsFromOneTag(Guid tagId)
    {
        var cards =  _cardTagsService.GetAllCardsByTagId(tagId);

        if (cards == null)
        {
            return NotFound();
        }

        _cardTagsService.DeleteAllCardTagFromOneTag(tagId);
        return NoContent();
    }
    
}