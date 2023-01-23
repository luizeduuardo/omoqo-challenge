namespace Omoqo.Challenge.Api.Models;

public class ErrorResponse
{
    public List<string> Errors { get; set; } = new List<string>();

    public ErrorResponse()
    {
    }

    public ErrorResponse(List<string> errors)
    {
        Errors = errors;
    }

    public ErrorResponse(Exception exception)
    {
        Errors = new List<string> { exception.Message };
    }
}