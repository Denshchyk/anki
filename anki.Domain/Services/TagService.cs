using anki.Domain.Interfases;
using anki.Domain.Models;
using anki.Domain.Repositories;

namespace anki.Domain.Services;

public class TagService : ITagService
{
    private ITagRepository _tagRepository;
    private ICardTagsRepository _cardTagsRepository;

    public TagService(ITagRepository tagRepository, ICardTagsRepository cardTagsRepository)
    {
        _tagRepository = tagRepository;
        _cardTagsRepository = cardTagsRepository;
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

    public async Task<TagModel> GetByTagIdAsync(string tagId)
    {
        var tag = await _tagRepository.GetByTagIdAsync(tagId);
        var guid = Guid.Parse(tagId);
        var cards = _cardTagsRepository.GetAllCardsByTagId(guid).Select(x=> x.Front).ToList();
        return new TagModel(tag.Name, cards);
    }

    public async Task<Tag?> GetTagByNameAsync(string name)
    {
        return await _tagRepository.GetByNameAsync(name);
    }

    public async Task<Tag> UpdateTagAsync(TagModel tag, string tagId)
    {
        var getTag = await _tagRepository.GetByTagIdAsync(tagId);
        ThrowExceptionIfTagNull(getTag);
        getTag.Name = tag.Name;
        await _tagRepository.UpdateTagAsync(getTag);
        return getTag;
    }
    public async Task AddTagAsync(Tag tag)
    {
        tag.TagId = Guid.NewGuid();

        await _tagRepository.AddTagAsync(tag);
    }
}