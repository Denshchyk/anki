using anki.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace anki.Tests;

[TestFixture]

public class TagRepositoryTests
{
    private DbContextOptions<ApplicationContext> _options;
    private ApplicationContext _dbContext;
    private TagRepository _tagRepository;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationContext(_options);
        _tagRepository = new TagRepository(_dbContext);
    }

    [Test]
    public async Task GetByTagIdAsync_GetExistingTag_ReturnsTag()
    {
        var tag = new Tag(Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07"), "name");
        await AddTestTagToInMemoryDb(tag);

        _tagRepository.GetByTagIdAsync(tag.TagId).Result?.Should().BeEquivalentTo(tag);
    }

    [Test]
    public async Task GetByNameAsync_GetExistingTag_ReturnsTag()
    {
        var tag = new Tag(Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07"), "name");
        await AddTestTagToInMemoryDb(tag);

        _tagRepository.GetByNameAsync(tag.Name).Result?.Should().BeEquivalentTo(tag);
    }

    [Test]
    public async Task RemoveTagAsync_DeleteTag_ReturnsNull()
    {
        var tag = new Tag(Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07"), "name");
        await AddTestTagToInMemoryDb(tag);

        await _tagRepository.RemoveTagAsync(tag);
        (await _dbContext.Tags.FindAsync(tag.TagId)).Should().Be(null);
    }

    [Test]
    public async Task UpdateTagAsync_UpdateCard_ReturnsNewTag()
    {
        var tag = new Tag(Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07"), "name");
        await AddTestTagToInMemoryDb(tag);

        var newTag = new Tag(Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07"), "newTag");
        await _tagRepository.UpdateTagAsync(newTag);

        _tagRepository.GetByTagIdAsync(tag.TagId).Result?.Should().BeEquivalentTo(newTag);
    }

    [Test]
    public async Task AddTagAsync_AddNewTag_FindTag()
    {
        var tag = new Tag(Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07"), "name");
        await _tagRepository.AddTagAsync(tag);

        (await _dbContext.Tags.FindAsync(tag.TagId)).Should().Be(tag);
    }
    
    private async Task AddTestTagToInMemoryDb(Tag tag)
    {
        await using var context = new ApplicationContext(_options);
        context.Tags.Add(tag);

        await context.SaveChangesAsync();
    }
}