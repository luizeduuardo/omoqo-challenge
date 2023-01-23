namespace Omoqo.Challenge.Api.Core.Models;

public class ShipAddModel
{
    public string Name { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public string Code { get; set; } = string.Empty;
}

public class ShipUpdateModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public string Code { get; set; } = string.Empty;
}