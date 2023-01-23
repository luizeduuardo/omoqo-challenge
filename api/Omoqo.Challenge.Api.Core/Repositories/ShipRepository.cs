namespace Omoqo.Challenge.Api.Core.Repositories;

public class ShipRepository : BaseRepository<Ship>, IShipRepository
{
    public ShipRepository(OmoqoContext context) : base(context) { }
}