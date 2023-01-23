namespace Omoqo.Challenge.Api.Core.Services;

public class ShipService : IShipService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IShipRepository _shipRepository;

    public ShipService(IUnitOfWork unitOfWork,
        IShipRepository shipRepository)
    {
        _unitOfWork = unitOfWork;
        _shipRepository = shipRepository;
    }

    public Result<Ship> Add(ShipAddModel model)
    {
        try
        {
            model.Name = model.Name.Trim();

            Ship ship = new Ship(model);

            if (!ship.IsValid)
                return new Result<Ship>(ship.Errors);

            Result addResult = _shipRepository.Add(ship);

            if (!addResult.Success)
                return new Result<Ship>(addResult.Errors);

            _unitOfWork.SaveChanges();

            return new Result<Ship>(ship);
        }
        catch (Exception ex)
        {
            return new Result<Ship>(ex);
        }
    }

    public async Task<Result<Ship>> UpdateAsync(ShipUpdateModel model)
    {
        try
        {
            model.Name = model.Name.Trim();

            Result<Ship?> selectResult =
                await _shipRepository.SingleOrDefaultAsync(model.Id);

            if (!selectResult.Success)
                return new Result<Ship>(selectResult.Errors);

            if (selectResult.Value == null)
                return new Result<Ship>($"Ship not found when updating it. Id: {model.Id}");

            selectResult.Value.Update(model);

            if (!selectResult.Value.IsValid)
                return new Result<Ship>(selectResult.Value.Errors);

            Result updateResult = _shipRepository.Update(selectResult.Value);

            if (!updateResult.Success)
                return new Result<Ship>(updateResult.Errors);

            _unitOfWork.SaveChanges();

            return new Result<Ship>(selectResult.Value);
        }
        catch (Exception ex)
        {
            return new Result<Ship>(ex);
        }
    }

    public async Task<Result> RemoveAsync(int id)
    {
        try
        {
            Result<Ship?> selectResult = await _shipRepository.SingleOrDefaultAsync(id);

            if (!selectResult.Success)
                return new Result(selectResult.Errors);

            if (selectResult.Value == null)
                return new Result($"Ship not found when removing it. Id: {id}");

            Result removeResult = _shipRepository.Remove(selectResult.Value);

            if (!removeResult.Success)
                return new Result(removeResult.Errors);

            _unitOfWork.SaveChanges();

            return new Result();
        }
        catch (Exception ex)
        {
            return new Result(ex);
        }
    }
}
