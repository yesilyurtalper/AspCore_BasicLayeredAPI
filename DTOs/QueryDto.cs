using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasicLayeredService.API.DTOs;

public class QueryDto
{
    public int? Id { get; set; } 
    public string? Title { get; set; }

    public string? Body { get; set; } 

    public string? Author { get; set; }

    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public double? PriceStart { get; set; }

    public double? PriceEnd { get; set; }

    public int? LastId { get; set; }

    public DateTime? LastDate { get; set; }
}
