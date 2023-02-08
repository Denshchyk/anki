using System.ComponentModel;
using anki.Domain;
using anki.Domain.Models;
using anki.Domain.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Microsoft.OpenApi.Any;
using Moq;

namespace anki.Tests;

[TestFixture]
public class CardRepositoryTests
{
    private ApplicationContext _dbContext;
    private CardRepository _cardRepository;
    private DbContextOptions<ApplicationContext> _options;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        _dbContext = new ApplicationContext(_options);
        _cardRepository = new CardRepository(_dbContext);
    }

    [Test]
    public async Task GetByIdAsync_GetExistingCard_ReturnsCard()
    {
        var card = new Card("cat", "kot") { Id = Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07") };
        await AddTestCardToInMemoryDb(card);
        
        _cardRepository.GetByIdAsync(card.Id).Result?.Should().BeEquivalentTo(card);
    }

    [Test]
    public async Task GetByFrontAndBackAsync_GetExistingCard_ReturnsCard()
    {
        var card = new Card("cat", "kot") { Id = Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07") };
        await AddTestCardToInMemoryDb(card);
        
        _cardRepository.GetByFrontAndBackAsync(card.Front, card.Back).Result?.Should().BeEquivalentTo(card);
    }

    [Test]
    public async Task RemoveCardAsync_DeleteCard_ReturnsNullAfterDelete()
    {
        var card = new Card("cat", "kot") { Id = Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07") };
        await AddTestCardToInMemoryDb(card);

        await _cardRepository.RemoveCardAsync(card);
        (await _dbContext.Cards.FindAsync(card.Id)).Should().Be(null);
    }

    [Test]
    public async Task UpdateCardAsync_UpdateCard_ReturnsNewCard()
    {
        var card = new Card("cat", "kot") { Id = Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07") };
        await AddTestCardToInMemoryDb(card);
        var updateCard = new Card("dog", "sobaka") { Id = Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07") };

        await _cardRepository.UpdateCardAsync(updateCard);
        
        _cardRepository.GetByIdAsync(card.Id).Result?.Should().BeEquivalentTo(updateCard);
    }

    [Test]
    public async Task AddCardAsync_AddNewCard_FindCard()
    {
        var card = new Card("cat", "kot") { Id = Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07") };
        await _cardRepository.AddCardAsync(card);

        (await _dbContext.Cards.FindAsync(card.Id)).Should().Be(card);
    }
 
    [Test]
    public async Task RemoveCardAsync_DeleteCard_CardNotFound()
    {
        var card = new Card("cat", "kot") { Id = Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07") };
        await AddTestCardToInMemoryDb(card);

        await _cardRepository.RemoveCardAsync(card);

        (await _dbContext.Cards.FindAsync(card.Id)).Should().Be(null);
    }
    
    private async Task AddTestCardToInMemoryDb(Card card)
    {
        await using var context = new ApplicationContext(_options);
        context.Cards.Add(card);
        
        await context.SaveChangesAsync();
    }
}