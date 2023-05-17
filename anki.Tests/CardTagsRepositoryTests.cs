using anki.Domain;
using anki.Domain.Models;
using anki.Domain.Repositories;
using anki.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace anki.Tests;

[TestFixture]
public class CardTagsRepositoryTests
{
    private DbContextOptions<ApplicationContext> _options;
    private ApplicationContext _dbContext;
    private CardTagsRepository _cardTagRepository;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationContext(_options);
        _cardTagRepository = new CardTagsRepository(_dbContext);
    }

    [Test]
    public async Task RemoveCardTagAsync_DeleteCardTag_ReturnsNull()
    {
        var guidCard = "81a130d2-502f-4cf1-a376-63edeb000e9f";
        var guidTag = "45965AD3-B77A-4B3A-B1C8-913848573D07";

        var cardTag = new CardTag(Guid.Parse(guidCard),Guid.Parse(guidTag));
        await AddTestCardTagToInMemoryDb(cardTag);

        await _cardTagRepository.DeleteCardTagAsync(cardTag);
        (await _dbContext.CardTags.FindAsync(cardTag.CardId, cardTag.TagId)).Should().Be(null);
    }

    [Test]
    public async Task AddCardTagAsync_AddNewCardTag_FindCardTag()
    {
        var guidCard = "81a130d2-502f-4cf1-a376-63edeb000e9f";
        var guidTag = "45965AD3-B77A-4B3A-B1C8-913848573D07";
        var cardTag = new CardTag(Guid.Parse(guidCard),Guid.Parse(guidTag));
        
        await _cardTagRepository.AddCardTagAsync(cardTag);
        (await _dbContext.CardTags.FindAsync(cardTag.CardId, cardTag.TagId)).Should().Be(cardTag);
    }

    [Test]
    public async Task GetAllCardsByTagId_GetCardList()
    {
        var guidCard = "81a130d2-502f-4cf1-a376-63edeb000e9f";
        var guidTag = "45965AD3-B77A-4B3A-B1C8-913848573D07";
        
        var cardTag = new CardTag(Guid.Parse(guidCard),Guid.Parse(guidTag));
        var cardTags = new List<CardTag?> { cardTag };
        await AddTestCardTagToInMemoryDb(cardTag);

        _cardTagRepository.GetAllCardsByTagId(cardTag.TagId).Should().HaveCount(1);
    }
    
    [Test]
    public async Task GetAllTagsByCardId_GetTagList()
    {
        var guidCard = "81a130d2-502f-4cf1-a376-63edeb000e9f";
        var guidTag = "45965AD3-B77A-4B3A-B1C8-913848573D07";
        
        var cardTag = new CardTag(Guid.Parse(guidCard),Guid.Parse(guidTag));
        var cardTags = new List<CardTag?> { cardTag };
        await AddTestCardTagToInMemoryDb(cardTag);

        _cardTagRepository.GetAllTagsByCardId(cardTag.CardId).Should().HaveCount(1);
    }
    
    private async Task AddTestCardTagToInMemoryDb(CardTag? cardTag)
    {
        await using var context = new ApplicationContext(_options);
        context.CardTags.Add(cardTag);
        
        var cardTags = new List<CardTag?> { cardTag };
        context.Cards.Add(new Card("front", "back") { CardTags = cardTags });
        context.Tags.Add(new Tag(Guid.Parse("45965AD3-B77A-4B3A-B1C8-913848573D07"), "name") { CardTags = cardTags });
        
        await context.SaveChangesAsync();
    }
}