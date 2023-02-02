using anki.Domain;
using FluentAssertions;
using Microsoft.OpenApi.Any;
using Moq;

namespace anki.Tests;

[TestFixture]
public class TagServiceTests
{
    private Mock<ITagRepository> _tagRepositoryMock;
    private TagService _tagService;

    [SetUp]
    public void Setup()
    {
        _tagRepositoryMock = new Mock<ITagRepository>();
        _tagService = new TagService(_tagRepositoryMock.Object);
    }
    
    [TestCase("testName")]
    public async Task GetByNameAsync_WithCorrectName_CallsRepository(string name)
    {
        await _tagService.GetTagByNameAsync(name);

        _tagRepositoryMock.Verify(t => t.GetByNameAsync(name), Times.Once);
    }

    [Test]
    public async Task GetByTagIdAsync_GetExistingTag_ReturnsTag()
    {
        var guid = "bc0ca7fa-60cc-4c61-8104-db001e4d885b";

        _tagRepositoryMock.Setup(x => x.GetByTagIdAsync(guid)).ReturnsAsync(new Tag(Guid.Parse(guid), "name"));

        (await _tagService.GetByTagIdAsync(guid))!.TagId.Should().Be(guid);
    }

    [Test]
    public async Task DeleteTagByIdAsync_RepositoryReturnsNull_ExceptionThrown()
    {
        var guid = "bc0ca7fa-60cc-4c61-8104-db001e4d885b";
        _tagRepositoryMock.Setup(x => x.GetByTagIdAsync(guid)).ReturnsAsync(value: null);
        await _tagService.Invoking(x => x.DeleteTagIdAsync(guid)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task DeleteTagByName_RepositoryReturnsTag_RepositoryMethodInvoked()
    {
        var guid = "bc0ca7fa-60cc-4c61-8104-db001e4d885b";
        var tag = new Tag(Guid.Parse(guid), "name");
        _tagRepositoryMock.Setup(x => x.GetByNameAsync(tag.Name)).ReturnsAsync(tag);

        await _tagService.DeleteTagByNameAsync(tag.Name);
        _tagRepositoryMock.Verify(x=> x.RemoveTagAsync(tag), Times.Once);
    }

    [Test]
    public async Task AddTagAsync_RepositoryReturnsTag()
    {
        var tag = new Tag(Guid.NewGuid(), "name");
        await _tagService.AddTagAsync(tag);
        _tagRepositoryMock.Verify(x=>x.AddTagAsync(tag), Times.Once);
    }

    [Test]
    public async Task UpdateTagAsync_RepositoryUpdateCard_ReturnsNewCard()
    {
        var guid = Guid.Parse("81a130d2-502f-4cf1-a376-63edeb000e9f");
        var tag = new Tag(guid, "name");

        _tagRepositoryMock.Setup(x => x.GetByTagIdAsync(guid)).ReturnsAsync(tag);

        await _tagService.UpdateTagAsync(tag);
        _tagRepositoryMock.Verify(x=> x.UpdateTagAsync(tag), Times.Once);
    }

    [Test]
    public void GetAll_RepositoryReturnsTagList()
    {
        var list = new List<Tag>() { new Tag(Guid.NewGuid(), "name") };
        _tagRepositoryMock.Setup(x => x.GetAll()).Returns(list);
        _tagService.GetAll().Should().Equal(list);
    }
}