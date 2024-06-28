using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasicLayeredService.API.DTOs;

public class EventQueryDto : BaseQueryDto
{
    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public double? PriceStart { get; set; }

    public double? PriceEnd { get; set; }

    public int SortByPrice { get; set; }

    public int SortByDate { get; set; }
}