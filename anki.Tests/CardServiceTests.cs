using anki.Domain;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace anki.Tests;

[TestFixture]
public class CardServiceTests
{
    private CardService _cardService;
    private Mock<ICardRepository> _cardRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _cardRepositoryMock = new Mock<ICardRepository>();
        _cardService = new CardService(_cardRepositoryMock.Object);
    }

    [TestCase("testFront", "testBack")]
    public async Task GetByFrontAndBackAsync_WithCorrectFrontAndBack_CallsRepository(string front, string back)
    {
        await _cardService.GetByFrontAndBackAsync(front, back);
        
        _cardRepositoryMock.Verify(x => x.GetByFrontAndBackAsync(front, back), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_GetExistingCard_ReturnsCard()
    {
        var guid = "81a130d2-502f-4cf1-a376-63edeb000e9f";
        
        _cardRepositoryMock.Setup(x => x.GetByIdAsync(guid))
            .ReturnsAsync(new Card("test", "test") { Id = Guid.Parse(guid) });

        (await _cardService.GetByIdAsync(guid))!.Id.Should().Be(guid);
    }
}