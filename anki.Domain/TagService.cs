namespace anki.Domain;

public class TagService
{
    private TagRepository _tagRepository;

    public TagService(TagRepository tagRepository)
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
        return await _tagRepository.GetByName(name);
    }

    public async Task<Tag> UpdateTagAsync(Tag tag)
    {
        var tagUpdate = await _tagRepository.GetByTagIdAsync(tag.TagId);
        ThrowExceptionIfTagNull(tagUpdate);
        await _tagRepository.UpdateTagAsync(tag);
        return tag;
    }
}