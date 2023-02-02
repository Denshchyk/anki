using Microsoft.EntityFrameworkCore;

namespace anki.Domain;

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
        var tag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(tag => tag.TagId == tagId);
        return tag;
    }
    
    public async Task<Tag?> GetByTagIdAsync(string tagId)
    {
        var guid = Guid.Parse(tagId);
        
        var tag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(tag => tag.TagId == guid);
        return tag;
    }

    public async Task<Tag?> GetByName(string name)
    {
        var tag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(tag => tag.Name == name);
        return tag;
    }

    public List<Tag> GetAll()
    {
        return _context.Tags.ToList();
    }
}