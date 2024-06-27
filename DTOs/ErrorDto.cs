namespace BasicLayeredService.API.DTOs;

public class ErrorDto 
{
    public string? Message { get; set; }
    public string? ResultCode { get; set; }
    public List<string>? ErrorMessages { get; set; }

    public ErrorDto(string? message = "",
         string? resultCode = "", List<string>? errorMessages = null)
    {
        Message = message;
        ResultCode = resultCode;
        ErrorMessages = errorMessages;
    }
}
