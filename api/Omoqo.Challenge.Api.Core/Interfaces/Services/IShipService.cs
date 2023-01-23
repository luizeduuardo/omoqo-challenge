namespace Omoqo.Challenge.Api.Core.Interfaces.Services;

public interface IShipService
{
    Result<Ship> Add(ShipAddModel model);
    Task<Result<Ship>> UpdateAsync(ShipUpdateModel model);
    Task<Result> RemoveAsync(int id);
}