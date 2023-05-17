using anki.Domain;
using anki.Domain.Interfases;
using anki.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace anki.Web.Controllers;

[ApiController]
[Route("[controller]")]

public class TagsController : ControllerBase
{
    private ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }
    [HttpGet]
    public IEnumerable<Tag> Get()
    {
        return _tagService.GetAll();
    }

    [HttpGet("{tagId}")]
    public async Task<ActionResult<Tag>> GetTag(string tagId)
    {
        var tag = await _tagService.GetByTagIdAsync(tagId);

        if (tag == null)
        {
            return NotFound();
        }
        return tag;
    }
    [HttpDelete("[action]/{tagId}")]
    
    public async Task<ActionResult<Tag>> DeleteTagId(string tagId)
    {
        var tag = await _tagService.DeleteTagIdAsync(tagId);
        
        if (tag == null)
        {
            return NotFound();
        }
        return tag;
    }
    
    [HttpDelete("[action]/{name}")]
    
    public async Task<ActionResult<Tag>> DeleteTagName(string name)
    {
        var tag = await _tagService.DeleteTagByNameAsync(name);
        
        if (tag.Name != name)
        {
            return BadRequest();
        }

        if (tag == null)
        {
            return NotFound();
        }

        return tag;
    }
    
    [HttpPut]
    public async Task<ActionResult<Tag>> UpdateTag(Tag tag)
    {
        if (tag == null)
        {
            return NotFound();
        }

        return await _tagService.UpdateTagAsync(tag);
    }

    [HttpPost("{name}")]
    public async Task AddTag(Guid tagId, string name)
    {
        await _tagService.AddTagAsync(tagId, name);
    }
}