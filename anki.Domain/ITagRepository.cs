namespace anki.Domain;

public interface ITagRepository
{
    Task AddTagAsync(Tag addTag);
    Task UpdateTagAsync(Tag tag);
    Task<Tag> RemoveTagAsync(Tag tag);
    Task<Tag?> GetByTagIdAsync(Guid tagId);
    Task<Tag?> GetByTagIdAsync(string tagId);
    Task<Tag?> GetByName(string name);
    List<Tag> GetAll();
}