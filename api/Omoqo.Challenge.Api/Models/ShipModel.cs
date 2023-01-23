namespace Omoqo.Challenge.Api.Models;

public class ShipGetResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public string Code { get; set; } = string.Empty;
}

public class ShipListRequest
{
    public int Skip { get; set; } = 0;
    public int Limit { get; set; } = 10;

    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class ShipListResponse
{
    public long Total { get; set; }
    public List<ShipListItemResponse>? Data { get; set; }
}

public class ShipListItemResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public string Code { get; set; } = string.Empty;
}

public class ShipAddRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public string Code { get; set; } = string.Empty;
}

public class ShipUpdateRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public string Code { get; set; } = string.Empty;
}