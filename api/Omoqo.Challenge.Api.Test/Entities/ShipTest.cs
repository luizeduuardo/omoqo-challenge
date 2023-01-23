namespace Omoqo.Challenge.Api.Test.Entities;

public class ShipTest
{
    [Fact]
    public void InstantiateShip_EmptyModel_ShouldReturnValidationMessages()
    {
        const string EMPTY_NAME_MESSAGE = "Name is required";
        const string EMPTY_CODE_MESSAGE = "Code is required";
        const string INVALID_LENGTH_MESSAGE = "Length must be greater than 0";
        const string INVALID_WIDTH_MESSAGE = "Width must be greater than 0";

        ShipAddModel model = new ShipAddModel();

        Ship ship = new Ship(model);

        Assert.False(ship.IsValid);
        Assert.Equal(4, ship.Errors.Count);
        Assert.Equal(EMPTY_NAME_MESSAGE, ship.Errors[0]);
        Assert.Equal(EMPTY_CODE_MESSAGE, ship.Errors[1]);
        Assert.Equal(INVALID_LENGTH_MESSAGE, ship.Errors[2]);
        Assert.Equal(INVALID_WIDTH_MESSAGE, ship.Errors[3]);
    }

    [Fact]
    public void InstantiateShip_EmptyName_ShouldReturnValidationMessage()
    {
        const string EMPTY_NAME_MESSAGE = "Name is required";

        ShipAddModel model = new ShipAddModel
        {
            Code = "AAAA-1111-A1"
        };

        Ship ship = new Ship(model);

        Assert.False(ship.IsValid);
        Assert.Equal(EMPTY_NAME_MESSAGE, ship.Errors[0]);
    }

    [Fact]
    public void InstantiateShip_EmptyCode_ShouldReturnValidationMessage()
    {
        const string EMPTY_CODE_MESSAGE = "Code is required";

        ShipAddModel model = new ShipAddModel
        {
            Name = "Ship 1"
        };

        Ship ship = new Ship(model);

        Assert.False(ship.IsValid);
        Assert.Equal(EMPTY_CODE_MESSAGE, ship.Errors[0]);
    }

    [Fact]
    public void InstantiateShip_InvalidCode_ShouldReturnValidationMessage()
    {
        const string INVALID_CODE_MESSAGE = "Code is invalid. You must use the pattern: 'AAAA-1111-A1'";

        ShipAddModel model = new ShipAddModel
        {
            Name = "Ship 1",
            Code = "AAAA-1111-11"
        };

        Ship ship = new Ship(model);

        Assert.False(ship.IsValid);
        Assert.Equal(INVALID_CODE_MESSAGE, ship.Errors[0]);
    }

    [Fact]
    public void InstantiateShip_InvalidLength_ShouldReturnValidationMessage()
    {
        const string INVALID_LENGTH_MESSAGE = "Length must be greater than 0";

        ShipAddModel model = new ShipAddModel
        {
            Name = "Ship 1",
            Code = "AAAA-1111-A1",
            Width = 10
        };

        Ship ship = new Ship(model);

        Assert.False(ship.IsValid);
        Assert.Equal(INVALID_LENGTH_MESSAGE, ship.Errors[0]);
    }

    [Fact]
    public void InstantiateShip_InvalidWidth_ShouldReturnValidationMessage()
    {
        const string INVALID_WIDTH_MESSAGE = "Width must be greater than 0";

        ShipAddModel model = new ShipAddModel
        {
            Name = "Ship 1",
            Code = "AAAA-1111-A1",
            Length = 10
        };

        Ship ship = new Ship(model);

        Assert.False(ship.IsValid);
        Assert.Equal(INVALID_WIDTH_MESSAGE, ship.Errors[0]);
    }

    [Fact]
    public void InstantiateShip_ValidModel_ShouldReturnIsValidTrue()
    {
        ShipAddModel model = new ShipAddModel
        {
            Name = "Ship 1",
            Code = "AAAA-1111-A1",
            Length = 10,
            Width = 10
        };

        Ship ship = new Ship(model);

        Assert.True(ship.IsValid);
    }
}