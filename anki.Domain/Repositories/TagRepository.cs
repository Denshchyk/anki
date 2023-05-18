using anki.Domain.Interfases;
using anki.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace anki.Domain.Repositories;

public class TagRepository : ITagRepository
{
    private ApplicationContext _context;

    public TagRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddTagAsync(Tag addTag)
    {
        await _context.Tags.AddAsync(addTag);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTagAsync(Tag tag)
    {
        _context.Tags.Update(tag);
        await _context.SaveChangesAsync();
    }

    public async Task<Tag> RemoveTagAsync(Tag tag)
    {
        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag?> GetByTagIdAsync(Guid tagId)
    {
        var tag = await _context.Tags.Include(x=>x.CardTags).ThenInclude(c=> c.Card).AsNoTracking().FirstOrDefaultAsync(tag => tag.TagId == tagId);
        return tag;
    }
    
    public async Task<Tag> GetByTagIdAsync(string tagId)
    {
        var guid = Guid.Parse(tagId);
        return await GetByTagIdAsync(guid);
    }

    public async Task<Tag?> GetByNameAsync(string name)
    {
        var tag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(tag => tag.Name == name);
        return tag;
    }

    public List<Tag> GetAll()
    {
        return _context.Tags.ToList();
    }
}