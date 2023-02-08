using anki.Domain.Interfases;
using anki.Domain.Models;

namespace anki.Domain.Services;

public class TagService : ITagService
{
    private ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task AddTagAsync(Guid tagId, string name)
    {
        var tag = new Tag(Guid.NewGuid(), name);
        await _tagRepository.AddTagAsync(tag);
    }

    public async Task<Tag> DeleteTagIdAsync(string tagId)
    {
        var tagDeleteId = await _tagRepository.GetByTagIdAsync(tagId);
        ThrowExceptionIfTagNull(tagDeleteId);
        await _tagRepository.RemoveTagAsync(tagDeleteId);
        return tagDeleteId;
    }

    public async Task<Tag> DeleteTagByNameAsync(string name)
    {
        var tagDeleteName = await _tagRepository.GetByNameAsync(name);
        ThrowExceptionIfTagNull(tagDeleteName);
        await _tagRepository.RemoveTagAsync(tagDeleteName);
        return tagDeleteName;
    }
    
    private static void ThrowExceptionIfTagNull(Tag? tagDeleteId)
    {
        if (tagDeleteId == null)
        {
            throw new NotFoundException("Tag is not found");
        }
    }

    public List<Tag> GetAll()
    {
        return _tagRepository.GetAll();
    }

    public async Task<Tag?> GetByTagIdAsync(string tagId)
    {
        return await _tagRepository.GetByTagIdAsync(tagId);
    }

    public async Task<Tag?> GetTagByNameAsync(string name)
    {
        return await _tagRepository.GetByNameAsync(name);
    }

    public async Task<Tag> UpdateTagAsync(Tag tag)
    {
        var tagUpdate = await _tagRepository.GetByTagIdAsync(tag.TagId);
        ThrowExceptionIfTagNull(tagUpdate);
        await _tagRepository.UpdateTagAsync(tag);
        return tag;
    }
    public async Task AddTagAsync(Tag tag)
    {
        tag.TagId = Guid.NewGuid();

        await _tagRepository.AddTagAsync(tag);
    }
}