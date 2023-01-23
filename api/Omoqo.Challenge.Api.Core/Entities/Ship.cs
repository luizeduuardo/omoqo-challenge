namespace Omoqo.Challenge.Api.Core.Entities;

public class Ship : BaseEntity
{
    private const string CODE_PATTERN = @"^[a-zA-Z]{4}-[0-9]{4}-[a-zA-Z]{1}[0-9]{1}$";

    protected Ship()
    {
    }

    public Ship(ShipAddModel model)
    {
        Name = model.Name;
        Length = model.Length;
        Width = model.Width;
        Code = model.Code;

        Validate();
    }

    public string Name { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public string Code { get; set; } = string.Empty;

    private void Validate()
    {
        if (string.IsNullOrEmpty(Name))
            Errors.Add("Name is required");

        if (string.IsNullOrEmpty(Code))
            Errors.Add("Code is required");
        else if (!Regex.IsMatch(Code, CODE_PATTERN, RegexOptions.None))
            Errors.Add("Code is invalid. You must use the pattern: 'AAAA-1111-A1'");

        if (Length <= 0)
            Errors.Add("Length must be greater than 0");

        if (Width <= 0)
            Errors.Add("Width must be greater than 0");
    }

    public void Update(ShipUpdateModel model)
    {
        Name = model.Name;
        Length = model.Length;
        Width = model.Width;
        Code = model.Code;

        Validate();
    }
}
