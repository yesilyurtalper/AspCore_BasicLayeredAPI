using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasicLayeredService.API.DTOs;

public class QueryDto
{
    public int? Id { get; set; } 
    public string? Title { get; set; }

    public string? Body { get; set; } 

    public string? Author { get; set; }

    public DateTime? Date { get; set; }

    public double? Price { get; set; }

}
