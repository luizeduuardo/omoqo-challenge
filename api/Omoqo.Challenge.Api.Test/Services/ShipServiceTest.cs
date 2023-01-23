namespace Omoqo.Challenge.Api.Test.Services;

public class ShipServiceTest
{
    private readonly IShipService _shipService;

    public ShipServiceTest()
    {
        DbContextOptions<OmoqoContext> contextOptions = new DbContextOptionsBuilder<OmoqoContext>()
            .UseInMemoryDatabase("OmoqoContextTest")
            .Options;

        OmoqoContext context = new OmoqoContext(contextOptions);

        IShipRepository shipRepository = new ShipRepository(context);
        IUnitOfWork unitOfWork = new UnitOfWork(context);

        _shipService = new ShipService(unitOfWork, shipRepository);

        using OmoqoContext scopedContext = new OmoqoContext(contextOptions);

        scopedContext.Database.EnsureDeleted();
        scopedContext.Database.EnsureCreated();

        scopedContext.Ships.AddRange(
            new Ship(new ShipAddModel { Name = "Ship 1", Code = "AAAA-1111-A1", Length = 10, Width = 10 }),
            new Ship(new ShipAddModel { Name = "Ship 2", Code = "AAAA-1111-A2", Length = 15, Width = 15 })
        );

        scopedContext.SaveChanges();
    }

    [Fact]
    public void Add_ValidModel_ShouldReturnNewEntity()
    {
        ShipAddModel model = new ShipAddModel
        {
            Name = "Ship 3",
            Code = "AAAA-1111-A3",
            Length = 10,
            Width = 10
        };

        Result<Ship> result = _shipService.Add(model);

        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Equal(3, result.Value.Id);
        Assert.Equal("Ship 3", result.Value.Name);
        Assert.Equal("AAAA-1111-A3", result.Value.Code);
    }

    [Fact]
    public async Task UpdateAsync_ShipNotFound_ShouldReturnErrorMessage()
    {
        const string NOTFOUND_SHIP_MESSAGE = "Ship not found when updating it. Id: 5";

        ShipUpdateModel model = new ShipUpdateModel
        {
            Id = 5
        };

        Result<Ship> result = await _shipService.UpdateAsync(model);

        Assert.False(result.Success);
        Assert.Equal(NOTFOUND_SHIP_MESSAGE, result.Errors[0]);
    }

    [Fact]
    public async Task UpdateAsync_ValidModel_ShouldReturnUpdatedEntity()
    {
        ShipUpdateModel model = new ShipUpdateModel
        {
            Id = 1,
            Name = "Ship 1.2",
            Code = "AAAA-1111-B1",
            Length = 10,
            Width = 10
        };

        Result<Ship> result = await _shipService.UpdateAsync(model);

        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Equal(1, result.Value.Id);
        Assert.Equal("Ship 1.2", result.Value.Name);
        Assert.Equal("AAAA-1111-B1", result.Value.Code);
    }

    [Fact]
    public async Task RemoveAsync_ShipNotFound_ShouldReturnErrorMessage()
    {
        const string NOTFOUND_SHIP_MESSAGE = "Ship not found when removing it. Id: 5";

        Result result = await _shipService.RemoveAsync(5);

        Assert.False(result.Success);
        Assert.Equal(NOTFOUND_SHIP_MESSAGE, result.Errors[0]);
    }

    [Fact]
    public async Task RemoveAsync_ValidId_ShouldReturnSuccess()
    {
        Result result = await _shipService.RemoveAsync(2);

        Assert.True(result.Success);
    }
}