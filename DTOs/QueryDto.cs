using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasicLayeredService.API.DTOs;

public class QueryDto
{
    public Guid? Id { get; set; } 
    public string? Title { get; set; }

    public string? Body { get; set; } 

    public string? Author { get; set; }

    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public double? PriceStart { get; set; }

    public double? PriceEnd { get; set; }

    public int PageSize { get; set; } = 5;
    public int PageNumber { get; set; } = 1;

    public bool Descending { get; set; } = false;
}
