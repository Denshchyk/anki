using anki.Domain.Models;

namespace anki.Domain.Interfases;

public interface ITagService
{
    Task AddTagAsync(Guid tagId, string name);
    Task AddTagAsync(Tag tag);
    Task<Tag> DeleteTagIdAsync(string tagId);
    Task<Tag> DeleteTagByNameAsync(string name);
    List<Tag> GetAll();
    Task<Tag?> GetByTagIdAsync(string tagId);
    Task<Tag?> GetTagByNameAsync(string name);
    Task<Tag> UpdateTagAsync(Tag tag);
}