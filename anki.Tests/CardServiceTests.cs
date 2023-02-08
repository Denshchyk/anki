using System.ComponentModel;
using anki.Domain;
using anki.Domain.Interfases;
using anki.Domain.Models;
using anki.Domain.Services;
using FluentAssertions;
using FluentAssertions.Formatting;
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

    [Test]
    public async Task DeleteCardByIdAsync_RepositoryReturnsNull_ExceptionThrown()
    {
        var guid = "81a130d2-502f-4cf1-a376-63edeb000e9f";
        _cardRepositoryMock.Setup(x => x.GetByIdAsync(guid)).ReturnsAsync(value: null);
        await _cardService.Invoking(x => x.DeleteCardIdAsync(guid)).Should().ThrowAsync<NotFoundException>();
    }
    
    [Test]
    public async Task DeleteCardByIdAsync_RepositoryReturnsCard_RepositoryMethodInvoked()
    {
        var guid = "81a130d2-502f-4cf1-a376-63edeb000e9f";
        var card = new Card(null, null) { Id = Guid.Parse(guid) };
        _cardRepositoryMock.Setup(x => x.GetByIdAsync(guid))
            .ReturnsAsync(card);

        await _cardService.DeleteCardIdAsync(guid);
        _cardRepositoryMock.Verify(x=> x.RemoveCardAsync(card), Times.Once);
    }

    [TestCase("testFront", "testBack")]
    public async Task DeleteCardByFrontAndBackAsync_RepositoryReturnsCard_RepositoryMethodInvoked(string front, string back)
    {
        var card = new Card(front, back);
        await _cardService.GetByFrontAndBackAsync(front, back);
        _cardRepositoryMock.Setup(x => x.GetByFrontAndBackAsync(front, back)).ReturnsAsync(card);
        
        await _cardService.DeleteCardAsync(card);
        _cardRepositoryMock.Verify(x=> x.RemoveCardAsync(card), Times.Once);
    }

    [Test]
    public async Task AddCardAsync_RepositoryReturnsCard()
    {
        var card = new Card("front", "back");
        await _cardService.AddCardAsync(card);
        _cardRepositoryMock.Verify(x => x.AddCardAsync(card), Times.Once);
    }

    [Test]
    public async Task UpdateCardAsync_RepositoryUpdateCard_ReturnsNewCard()
    {
        var guid = Guid.Parse("81a130d2-502f-4cf1-a376-63edeb000e9f");
        var card = new Card(null, null) { Id = guid };
        _cardRepositoryMock.Setup(x => x.GetByIdAsync(guid))
            .ReturnsAsync(card);

        await _cardService.UpdateCardAsync(card);
        _cardRepositoryMock.Verify(x=> x.UpdateCardAsync(card), Times.Once);
    }

    [Test]
    public void GetAll_RepositoryReturnsCardList()
    {
        var list = new List<Card>() { new Card("front", "back") };
        _cardRepositoryMock.Setup(x=> x.GetAll()).Returns(list) ;
        _cardService.GetAll().Should().Equal(list);
    }
}