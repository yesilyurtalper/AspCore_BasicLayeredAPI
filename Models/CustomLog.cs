using BasicLayeredService.API.DTOs;

namespace BasicLayeredService.API.Models;

public class CustomLog
{
    public string Method { get; set; }
    public string Path { get; set; }
    public ResponseDtoBase Result { get; set; }
    public DateTime? Created { get; set; }  = DateTime.Now;
    public string User { get; set; }
}
